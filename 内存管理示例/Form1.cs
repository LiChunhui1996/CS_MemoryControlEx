using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 内存管理示例
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       // int[] list = {1, 0, 2, 2, 1, 7, 6, 7, 0, 1, 2, 0, 3, 0, 4, 5, 1, 5, 2, 4, 5, 6, 7, 6, 7, 2, 4, 2, 7, 3, 3, 2, 3, };
       //1 0 2 2 1 7 6 7 0 1 2 0 3 0 4 5 1 5 2 4 5 6 7 6 7 2 4 2 7 3 3 2 3
        MyStack myStack = new MyStack(4);


        private void EXECUTE_Click(object sender, EventArgs e)
        {
            if (listBox.Text != "")
            {

                string[] listString = listBox.Text.Split(' ');
                Console.WriteLine(listString);
                int[] list = new int[listString.Length];
                for (int z = 0; z < listString.Length; z++)
                {
                    list[z] = Convert.ToInt32(listString[z]);
                }
                Console.WriteLine(list);

                if (radioButton1.Checked == true)
                {
                    textBox.AppendText("LRU list\r\n#########\r\n");

                    for (int i = 0; i < list.Length; i++)
                    {
                        //Thread.Sleep(200);
                        textBox.AppendText(myStack.PushLRU(list[i]));
                        string[] ans = myStack.printStack();
                        textBox2.AppendText(ans[1]);
                        textBox.AppendText(ans[0]);
                        textBox1.Text = "";
                        int count = int.Parse(ans[2]);
                        textBox1.AppendText("中断次数：" + ans[2] + "        命中率：" + (i+1-count ) + "/" + (i + 1));
                        textBox3.AppendText("############################\r\n当前系统申请的块是：" + list[i] + "\r\n" + "############################\r\n");
                        textBox3.AppendText(ans[3]);
                        textBox3.AppendText("当前内存占用块：" + ans[4] + "\r\n\r\n");
                    }
                }
                else if (radioButton1.Checked == false)
                {

                    textBox.AppendText("FIFO list\r\n#########\r\n");
                    for (int i = 0; i < list.Length; i++)
                    {
                        //Thread.Sleep(200);
                        textBox.AppendText(myStack.PushFIFO(list[i]));
                        string[] ans = myStack.printStack();
                        textBox2.AppendText(ans[1]);
                        textBox.AppendText(ans[0]);
                        textBox1.Text = "";
                        int count = int.Parse(ans[2]);
                        textBox1.AppendText("中断次数：" + ans[2]+"              命中率："+(i+1-count)+"/"+(i+1));
                        textBox3.AppendText("############################\r\n当前系统申请的块是："+list[i]+"\r\n"+ "############################\r\n");
                        textBox3.AppendText(ans[3]);
                        textBox3.AppendText("当前内存占用块："+ans[4]+"\r\n\r\n");
                    }
                }
            }
            else 
            {
                MessageBox.Show("请添加一个list序列", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                radioButton1.Checked = true;
                radioButton1.Text = "LRU";
        }

    }
}
