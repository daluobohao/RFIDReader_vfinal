using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Sjtu.Crypto
{
    class Trivium
    {

        protected int _currentIndex;

        protected static int KEY_SIZE_BITS = 80;
        protected static int IV_SIZE_BITS = 80;

        protected byte[] key = new byte[10];
        protected byte[] iv = new byte[10];
        protected int[] s = new int[10];

        public int currentIndex()
        {
            return _currentIndex;
        }

        public static int rightMove(int value, int pos)
        {
            if (pos > 0)
            {
                int mask = 0x7fffffff;
                value >>= 1;
                value &= mask;
                value >>= pos - 1;
            }
            return value;
        }

        public void run(byte[] buf)
        {
            this.run(_currentIndex, buf);
        }

        public void run(int fromIndex, byte[] buf)
        {
            if (_currentIndex > fromIndex)
            {
                this.initProcess();
                this.run(fromIndex, buf);
            }
            else
            {
                int inOfs = 0, outOfs = 0, len = buf.Length + fromIndex - _currentIndex;
                int s11 = s[0];
                int s12 = s[1];
                int s13 = s[2];
                int s21 = s[3];
                int s22 = s[4];
                int s23 = s[5];
                int s31 = s[6];
                int s32 = s[7];
                int s33 = s[8];
                int s34 = s[9];

                int outEnd = outOfs + (len & ~3);

                for (; outOfs < outEnd; outOfs += 4, inOfs += 4)
                {
                    int t1, t2, t3, reg;

                    t1 = ((s13 << 96 - 66) | rightMove(s12, 66 - 64)) ^ ((s13 << 96 - 93) | rightMove(s12, 93 - 64));
                    t2 = ((s23 << 96 - 69) | rightMove(s22, 69 - 64)) ^ ((s23 << 96 - 84) | rightMove(s22, 84 - 64));
                    t3 = ((s33 << 96 - 66) | rightMove(s32, 66 - 64)) ^ ((s34 << 128 - 111) | rightMove(s33, 111 - 96));

                    reg = t1 ^ t2 ^ t3;
                    try
                    {
                        buf[outOfs + _currentIndex - fromIndex + 3] = (byte)(buf[inOfs + _currentIndex - fromIndex + 3] ^ reg >> 24);
                        buf[outOfs + _currentIndex - fromIndex + 2] = (byte)(buf[inOfs + _currentIndex - fromIndex + 2] ^ reg >> 16);
                        buf[outOfs + _currentIndex - fromIndex + 1] = (byte)(buf[inOfs + _currentIndex - fromIndex + 1] ^ reg >> 8);
                        buf[outOfs + _currentIndex - fromIndex] = (byte)(buf[inOfs + _currentIndex - fromIndex] ^ reg);

                    }
                    catch (Exception e)
                    {
                    }


                    t1 ^= (((s13 << 96 - 91) | rightMove(s12, 91 - 64)) & ((s13 << 96 - 92) | rightMove(s12, 92 - 64))) ^ ((s23 << 96 - 78) | rightMove(s22, 78 - 64));
                    t2 ^= (((s23 << 96 - 82) | rightMove(s22, 82 - 64)) & ((s23 << 96 - 83) | rightMove(s22, 83 - 64))) ^ ((s33 << 96 - 87) | rightMove(s32, 87 - 64));
                    t3 ^= (((s34 << 128 - 109) | rightMove(s33, 109 - 96)) & ((s34 << 128 - 110) | rightMove(s33, 110 - 96))) ^ ((s13 << 96 - 69) | rightMove(s12, 69 - 64));

                    s13 = s12; s12 = s11; s11 = t3;
                    s23 = s22; s22 = s21; s21 = t1;
                    s34 = s33; s33 = s32; s32 = s31; s31 = t2;
                }

                // NOTE: could save some code memory by merging the two blocks, but that
                // would decrease the speed because of additional conditional jumps...
                outEnd = outOfs + (len & 3);
                if (0 < outEnd)
                {
                    int t1, t2, t3, reg;

                    t1 = ((s13 << 96 - 66) | rightMove(s12, 66 - 64)) ^ ((s13 << 96 - 93) | rightMove(s12, 93 - 64));
                    t2 = ((s23 << 96 - 69) | rightMove(s22, 69 - 64)) ^ ((s23 << 96 - 84) | rightMove(s22, 84 - 64));
                    t3 = ((s33 << 96 - 66) | rightMove(s32, 66 - 64)) ^ ((s34 << 128 - 111) | rightMove(s33, 111 - 96));

                    reg = t1 ^ t2 ^ t3;
                    for (; outOfs < outEnd; outOfs++, inOfs++)
                    {
                        try
                        {
                            buf[outOfs + _currentIndex - fromIndex] = (byte)(buf[inOfs + _currentIndex - fromIndex] ^ reg);
                        }
                        catch (Exception e)
                        {
                        }
                        reg >>= 8;
                    }

                    t1 ^= (((s13 << 96 - 91) | rightMove(s12, 91 - 64)) & ((s13 << 96 - 92) | rightMove(s12, 92 - 64))) ^ ((s23 << 96 - 78) | rightMove(s22, 78 - 64));
                    t2 ^= (((s23 << 96 - 82) | rightMove(s22, 82 - 64)) & ((s23 << 96 - 83) | rightMove(s22, 83 - 64))) ^ ((s33 << 96 - 87) | rightMove(s32, 87 - 64));
                    t3 ^= (((s34 << 128 - 109) | rightMove(s33, 109 - 96)) & ((s34 << 128 - 110) | rightMove(s33, 110 - 96))) ^ ((s13 << 96 - 69) | rightMove(s12, 69 - 64));

                    s13 = s12; s12 = s11; s11 = t3;
                    s23 = s22; s22 = s21; s21 = t1;
                    s34 = s33; s33 = s32; s32 = s31; s31 = t2;
                }

                s[0] = s11;
                s[1] = s12;
                s[2] = s13;
                s[3] = s21;
                s[4] = s22;
                s[5] = s23;
                s[6] = s31;
                s[7] = s32;
                s[8] = s33;
                s[9] = s34;
                _currentIndex = fromIndex + buf.Length;
            }

        }

        public void setK(byte[] key, int ofs)
        {
            Array.Copy(key, ofs, this.key, 0, this.key.Length);
        }

        public void setK(byte[] key)
        {
            this.setK(key, 0);
        }

        public void setIV(byte[] iv, int ofs)
        {
            Array.Copy(iv, ofs, this.iv, 0, this.iv.Length);
        }

        public void setIV(byte[] iv)
        {
            this.setIV(iv, 0);
        }

        public void setKIV(byte[] bytes)
        {
            this.setK(bytes);
            this.setIV(bytes, 10);
        }

        public static int readInt32LE(byte[] data, int ofs)
        {
            return (data[ofs + 3] << 24) |
                   ((data[ofs + 2] & 0xff) << 16) |
                   ((data[ofs + 1] & 0xff) << 8) |
                    (data[ofs] & 0xff);
        }

        public void initProcess()
        {
            byte[] key = this.key;
            byte[] nonce = this.iv;
            int[] s = this.s;

            int s11 = readInt32LE(key, 0);
            int s12 = readInt32LE(key, 4);
            int s13 = (key[8] & 0x0ff) |
                      ((key[9] << 8) & 0x0ff00);
            int s21 = readInt32LE(nonce, 0);
            int s22 = readInt32LE(nonce, 4);
            int s23 = (nonce[8] & 0x0ff) |
                      ((nonce[9] << 8) & 0x0ff00);
            int s31 = 0;
            int s32 = 0;
            int s33 = 0;
            int s34 = 0x07000;


            ///////////////!!!!!!!!!!!!!!!!!!!!!!!!
            //		  System.out.printf(
            //				  "s11=%08x\n" +
            //				  "s12=%08x\n" +
            //				  "s13=%08x\n" +
            //				  "s21=%08x\n" +
            //				  "s22=%08x\n" +
            //				  "s23=%08x\n" +
            //				  "s31=%08x\n" +
            //				  "s32=%08x\n" +
            //				  "s33=%08x\n" +
            //				  "s34=%08x\n",
            //					s11, s12, s13,
            //					s21, s22, s23,
            //					s31, s32, s33, s34);		


            for (int i = 0; i < 4 * 3 * 96; i++)
            {
                int t1, t2, t3;

                t1 = ((s13 << 96 - 66) | rightMove(s12, 66 - 64)) ^ ((s13 << 96 - 93) | rightMove(s12, 93 - 64));
                t2 = ((s23 << 96 - 69) | rightMove(s22, 69 - 64)) ^ ((s23 << 96 - 84) | rightMove(s22, 84 - 64));
                t3 = ((s33 << 96 - 66) | rightMove(s32, 66 - 64)) ^ ((s34 << 128 - 111) | rightMove(s33, 111 - 96));

                t1 ^= (((s13 << 96 - 91) | rightMove(s12, 91 - 64)) & ((s13 << 96 - 92) | rightMove(s12, 92 - 64))) ^ ((s23 << 96 - 78) | rightMove(s22, 78 - 64));
                t2 ^= (((s23 << 96 - 82) | rightMove(s22, 82 - 64)) & ((s23 << 96 - 83) | rightMove(s22, 83 - 64))) ^ ((s33 << 96 - 87) | rightMove(s32, 87 - 64));
                t3 ^= (((s34 << 128 - 109) | rightMove(s33, 109 - 96)) & ((s34 << 128 - 110) | rightMove(s33, 110 - 96))) ^ ((s13 << 96 - 69) | rightMove(s12, 69 - 64));

                s13 = s12; s12 = s11; s11 = t3;
                s23 = s22; s22 = s21; s21 = t1;
                s34 = s33; s33 = s32; s32 = s31; s31 = t2;
            }

            s[0] = s11;
            s[1] = s12;
            s[2] = s13;
            s[3] = s21;
            s[4] = s22;
            s[5] = s23;
            s[6] = s31;
            s[7] = s32;
            s[8] = s33;
            s[9] = s34;

            _currentIndex = 0;

        }




    }
}
