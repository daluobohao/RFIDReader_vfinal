using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;

using System.Net;
using System.Net.Sockets;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;
using System.Threading;
using Com.Sjtu.Crypto;
using Apache.NMS;

namespace RFIDReaderMiddleware.Crypto
{
    class KeyAgreement
    {

        protected FpCurve _curve;
        protected ECDomainParameters _ecSpec;
        protected IAsymmetricCipherKeyPairGenerator _keyGen;

        //protected byte[] _keyBytes;
        protected string _id;
        protected Trivium _trivium;

        public KeyAgreement()
        {
            _curve = new FpCurve(
                new BigInteger("BDB6F4FE3E8B1D9E0DA8C0D46F4C318CEFE4AFE3B6B8551F", 16), // q
                new BigInteger("BB8E5E8FBC115E139FE6A814FE48AAA6F0ADA1AA5DF91985", 16), // a
                new BigInteger("1854BEBDC31B21B7AEFC80AB0ECD10D5B1B3308E6DBF11C1", 16)  // b
                );

            _ecSpec = new ECDomainParameters(
                _curve,
                new FpPoint(_curve,
                        new FpFieldElement(
                            _curve.Q,
                            new BigInteger("4AD5F7048DE709AD51236DE65E4D4B482C836DC6E4106640", 16)
                        ),
                        new FpFieldElement(
                            _curve.Q,
                            new BigInteger("02BB3A02D4AAADACAE24817A4CA3A1B014B5270432DB27D2", 16))
                        ), // G
                        new BigInteger("BDB6F4FE3E8B1D9E0DA8C0D40FC962195DFAE76F56564677", 16), // n
                        BigInteger.One// h
                );
            _keyGen = GeneratorUtilities.GetKeyPairGenerator("ECDH");
            _keyGen.Init(new ECKeyGenerationParameters(_ecSpec, new SecureRandom()));

        }

        public void start()
        {
            while (true)
            {
                try
                {
                    this.run();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
            }
        }

        public void run()
        {
            
            AsymmetricCipherKeyPair aKeyPair = _keyGen.GenerateKeyPair();
            IBasicAgreement aKeyAgreeBasic = AgreementUtilities.GetBasicAgreement("ECDH");
            aKeyAgreeBasic.Init(aKeyPair.Private);
            byte[] pubEnc = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(aKeyPair.Public).GetDerEncoded();
            
            IPAddress ip=IPAddress.Parse("192.168.1.117");
            IPEndPoint ipe = new IPEndPoint(ip, 8899);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipe);
            //byte[] bs = Encoding.ASCII.GetBytes("Windows Test");
            socket.Send(pubEnc, pubEnc.Length, 0);

            byte[] recvBytes = new byte[1024];
            int bytes=socket.Receive(recvBytes, recvBytes.Length, 0);

            socket.Close();

            byte[] bPub = new byte[bytes];
            for (int i = 0; i < bytes; i++)
            {
                bPub[i] = recvBytes[i];
            }
            ECPublicKeyParameters pubKey = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(bPub);
            BigInteger k = aKeyAgreeBasic.CalculateAgreement(pubKey);
            byte[] keyBytes = k.ToByteArray();
            //Console.WriteLine(keybytes.Length);
            if (keyBytes.Length > 24)
            {
                byte[] fixedKeybytes = new byte[24];
                for (int i = 0; i < 24; i++)
                {
                    fixedKeybytes[i] = keyBytes[i + keyBytes.Length - 24];
                }
                keyBytes = fixedKeybytes;
            }
            Console.WriteLine(Hex.ToHexString(keyBytes));
            
            IDigest digest = new MD5Digest();
            digest.BlockUpdate(keyBytes, 0, keyBytes.Length);
            byte[] idBytes = new byte[digest.GetDigestSize()];
            digest.DoFinal(idBytes, 0);
            _id = Hex.ToHexString(idBytes);
            Console.WriteLine(_id);
            _trivium = new Trivium();
            _trivium.setKIV(keyBytes);
            _trivium.initProcess();
        }

        public void writeMessage(IBytesMessage msg, byte[] bytes)
        {
            msg.Properties.SetString("id", _id);
            msg.Properties.SetInt("index", _trivium.currentIndex());
            _trivium.run(bytes);
            msg.WriteBytes(bytes);
        }
    }
}
