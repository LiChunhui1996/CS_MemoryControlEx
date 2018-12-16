using System;

namespace 内存管理示例
{
    class MyStack
    {
        public int num;
        public int[] stack;
        public int point;
        int count ;

        int[,] v = new int[8, 7];

        int[] virtualV = { 100, 102, 104, 106, 108, 110, 112, 114 };
        int[,] divide = new int[64, 2];

        public MyStack(int num)
        {
            this.num = num;
            stack = new int[num];
            point = -1;
            count = 0;

            Random random = new Random();
            int[] physicsV = new int[15];
            for (int i=0;i<15;i++)
            {
                physicsV[i]= random.Next(0, 63);
            }

            for (int i=0;i<64;i++)
            {
                divide[i, 0] = i;
                int flag = 0;
                for (int j=0;j<15;j++)
                {
                    if (physicsV[j] == i)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    divide[i, 1] = 0;
                }else
                {
                    divide[i, 1] = 1;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                        v[i, 0] = i;
                        v[i, 1] = 0;
                        v[i, 2] = 0;
                        v[i, 3] = 0;
                        v[i, 4] = 0;
                        v[i, 5] = virtualV[i];
                
            }

        }

        private int getNum()
        {
            int i = 0;
            while(i<64)
            {
                if (divide[i, 1] == 0)
                {
                    divide[i, 1] = 1;
                    return i;

                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public void print()
        {
            Console.WriteLine(num);
            Console.WriteLine(stack.Length);
        }

        public string[] printStack()
        {
            string[] stutes=new string[5];

            stutes[1] = "页号\t物理块\t段号\t状态位P\t访问位A\t修改位M\t外存地址\t段号\r\n";
            stutes[0] ="\r\nP----N-\r\n";
            Console.WriteLine("---------");
            for (int i = point; i >= 0; i--)
            {
                stutes[0] ="" +stutes[0] + stack[i]+ "    ";
                Console.WriteLine(stack[i]);

                stutes[3] = "";
                int countNum = 0;
                for (int m = 0; m < 64;m=m+2)
                {    
                    stutes[3] = stutes[3]+divide[m,0]+"\t "+divide[m,1]+"\t "+ divide[m+1, 0] + "\t " + divide[m+1, 1] + "\t\r\n";
                    if ((divide[m, 1] == 1)|| (divide[m+1, 1]==1))
                    {
                        countNum++;
                    }
                }
                stutes[4] = countNum.ToString();
                if (point == 3)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if ((stack[3] != v[x, 0]) && (stack[2] != v[x, 0]) && (stack[1] != v[x, 0]) && (stack[0] != v[x, 0]))
                        {
                            v[x, 1] = 0;
                           
                                //if (divide[x,1]==1)
                                //{
                                //divide[x, 1] = 0;
                                //}
                          
                            v[x, 2] = 0;
                            v[x, 3] = 0;
                            v[x, 4] = 0;
                            v[x, 5] = virtualV[x];
                            v[x, 6] = 0;
                        }
                    }

                }
                if (point == 2)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if ((stack[2] != v[x, 0]) && (stack[1] != v[x, 0]) && (stack[0] != v[x, 0]))
                        {
                            v[x, 1] = 0;
                            //if (divide[x, 1] == 1)
                            //{
                            //    divide[x, 1] = 0;
                            //}
                            v[x, 2] = 0;
                            v[x, 3] = 0;
                            v[x, 4] = 0;
                            v[x, 5] = virtualV[x];
                            v[x, 6] = 0;
                        }
                    }
                }
                if (point == 1)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if ((stack[1] != v[x, 0]) && (stack[0] != v[x, 0]))
                        {
                            v[x, 1] = 0;
                            //if (divide[x, 1] == 1)
                            //{
                            //    divide[x, 1] = 0;
                            //}
                            v[x, 2] = 0;
                            v[x, 3] = 0;
                            v[x, 4] = 0;
                            v[x, 5] = virtualV[x];
                            v[x, 6] = 0;
                        }
                    }
                }
                if (point == 0)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (stack[0] != v[x, 0])
                        {
                            v[x, 2] = 0;
                            v[x, 3] = 0;
                            v[x, 4] = 0;
                            v[x, 5] = virtualV[x];
                            v[x, 6] = 0;
                        }
                    }
                }

                for (int x = 0; x < 8; x++)
                {
                    if (stack[i] == v[x, 0])
                    {
                        if (v[x, 1] == 0)
                        {
                            v[x, 1] = getNum();
                            Random r = new Random();
                            v[x, 6] = r.Next(0, 64);
                        }


                        v[x, 2] = 1;
                        v[x, 3] = 1;
                        v[x, 4] = 1;
                        v[x, 5] = 0;

                        stutes[0] = stutes[0] + v[x, 1] + "\r\n";
                    }
                }
            }
    
            stutes[0] = stutes[0] + "=========\r\n";
            Console.WriteLine("=========");

            stutes[1] = "页号\t物理块\t状态位P\t访问位A\t修改位M\t外存号\t段号\r\n";
            for (int m=0;m<8;m++)
            {
                for (int n=0;n<7;n++)
                {
                    stutes[1] = stutes[1] + v[m,n] + "\t";
                }
                stutes[1] = stutes[1] + "\r\n";
            }
            stutes[1] = stutes[1] + "--------------------------------------------------------\r\n";
            stutes[2] =count.ToString();
            return stutes;

        }

        public int getStackLength()
        {
            return point + 1;
        }

        public void clearStack()
        {
            point = -1;
        }

        public int getTop()
        {
            return stack[point];
        }


        public bool isEmpty()
        {
            if (point == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isFull()
        {
            if (point == num - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Push(int aim)
        {
            if (isFull())
            {
                Console.WriteLine("Stack is full !");
                return;
            }
            else
            {
                point = point + 1;
                //            System.out.println(point);
                stack[point] = aim;
                //for (int x=0;x<8;x++)
                //{
                //    if (v[x, 0] == aim)
                //    {
                //        v[x, 2] = 1;
                //        v[x, 3] = 1;
                //        v[x, 4] = 1;
                //    }
                //}


            }
        }

        public int Pop()
        {
            if (isEmpty())
            {
                Console.WriteLine("Stack is Empty!");
                return -999;
            }
            else
            {
                int back = stack[point];
                point--;
                //for (int x = 0; x < 8; x++)
                //{
                //    if (v[x, 0] == back)
                //    {
                //        v[x, 2] = 0;
                //        v[x, 3] = 0;
                //        v[x, 4] = 0;
                //    }
                //}
                return back;

            }
        }

        public void PopIndex(int aim)
        {
            int index = 0;
            for (int i = 0; i <= point; i++)
            {
                if (stack[i] == aim)
                {
                    index = i;
                }
            }
            for (int i = index; i < point; i++)
            {
                stack[i] = stack[i + 1];
            }
            //for (int x = 0; x < 8; x++)
            //{
            //    if (v[x, 0] == aim)
            //    {
            //        v[x, 2] = 0;
            //        v[x, 3] = 0;
            //        v[x, 4] = 0;
            //    }
            //}
            point = point - 1;
        }

        public bool hasAim(int aim)
        {
            for (int i = 0; i <= point; i++)
            {
                if (aim == stack[i])
                {
                    return true;
                }
            }
            return false;
        }

        private int giveBottom()
        {
            int bottom = stack[point];
            for (int i = 0; i < point; i++)
            {
                stack[i] = stack[i + 1];
            }
            point = point - 1;
            //for (int x = 0; x < 8; x++)
            //{
            //    if (v[x, 0] == bottom)
            //    {
            //        v[x, 2] = 0;
            //        v[x, 3] = 0;
            //        v[x, 4] = 0;
            //    }
            //}
            return bottom;
        }

        private int changeList()
        {
            int bottom = stack[0];
            for (int i = 0; i < point; i++)
            {
                stack[i] = stack[i + 1];
            }
            stack[point] = bottom;
            return bottom;
        }

        //FIFO的函数

        public string PushFIFO(int aim)
        {
            string text;
            text = "####" + aim + "####";

            if (!isFull() && !hasAim(aim))
            {  //栈没有满，并且栈里面没有aim，直接Push就行
                this.Push(aim);
                count++;
            }
            else if (isFull() && !hasAim(aim))
            {  //栈满了，但是没有aim，把栈底去掉，把aim放栈顶
                this.giveBottom();
                this.Push(aim);
                count++;
            }
            else if (!isFull() && hasAim(aim))
            {  //栈没有满，但是有aim

            }
            else if (isFull() && hasAim(aim))
            {  //栈满了，但是有aim

            }

            return text;
        }

        //LRU函数
        public string PushLRU(int aim)
        {
            string text;
            text = "####" + aim + "####";

            Console.WriteLine("####" + aim + "####");
            if (!isFull() && !hasAim(aim))
            {  //栈没有满，并且栈里面没有aim，直接Push就行
                this.Push(aim);
                count++;
            }
            else if (isFull() && !hasAim(aim))
            {  //栈满了，但是没有aim
                this.giveBottom();
                this.Push(aim);
                count++;
            }
            else if (!isFull() && hasAim(aim))
            {  //栈没有满，但是有aim，将aim置为栈顶，
                if (getTop() == aim)
                {

                }
                else
                {
                    this.changeList();
                }

            }
            else if (isFull() && hasAim(aim))
            {  //栈满了，但是有aim
                if (this.getTop() == aim)
                {
                }
                else
                {
                    this.PopIndex(aim);
                    this.Push(aim);
                }
            }
            return text;
        }
    }
}