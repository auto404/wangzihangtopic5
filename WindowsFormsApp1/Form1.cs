using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public class Point
        {// 二维坐标上的点Point
            public int x;
            public int y;
            public int weight;
        }

        public class Node
        {// 二维坐标上的点Point
            public bool up=true;
            public bool right=true;
            public int  upNum=0;
            public int rightNum=0;
        }
        public int sum;
       
        public Stack<Point> path=new Stack<Point>();
        public Stack<Point> temp = new Stack<Point>();
        public Stack<Point> best = new Stack<Point>();
        public int bestWeight;
        public int bestNum;
        int m, n;//终点坐标
        public List<List<Node>> nodes = new List<List<Node>>();
        //初始化节点表格,某单元格的表示为（nodes[y][x]）上为y正方向，右为x正方向
        void getform()
        {
           
            for (int i = 0; i <= m; i++)
            {
                 Random rd = new Random(Guid.NewGuid().GetHashCode());
                 List<Node> list = new List<Node>();
                 for (int j = 0; j <=n; j++)
                 {
                    
                    Node node = new Node();
                    node.rightNum=rd.Next(0,101);
                    node.upNum=rd.Next(0,101);
                    if(node.upNum==0)
                    {
                        node.up = false;
                    }
                    if (node.rightNum == 0)
                    {
                        node.right = false;
                    }
                    if (j == n)
                    {
                        node.upNum = 0;
                       node.up = false;
                    }
                        
                   if(i==m)
                    {
                        node.rightNum = 0;
                       node.right = false;
                    }
                       
                   list.Add(node);
                  }
                   nodes.Add(list);
            }
           
        }
        void getSum(int a, int b)
        {

            if (m== a && n == b)
            {
                int weight=0;
        
                Point p = new Point();
                p.x = a;
                p.y = b;
                path.Push(p);//让当前点进栈
                listBox1.Items.Add("******************路径" + ++sum + "******************" );
               
                while (path.Count!=0)//将path里面的点取出来，放在temp里面
                {//path从栈顶-栈底的方向，路径是从终点-起点的顺序
                    Point p1 = path.Pop();
                    temp.Push(p1);
                }
                while (temp.Count!=0)
                {//输出temp里面的路径，这样刚好是从起点到终点的顺序
                    Point p1 = temp.Pop();
                    path.Push(p1);//将路径放回path里面，因为后面还要回溯!!!
                    listBox1.Items.Add("(" + p1.x + "," +p1.y + ")");
                   
                    weight += p1.weight;
                }
               
                listBox1.Items.Add("权重" + weight);
                if (sum == 1)
                {
                    bestWeight = weight;
                    bestNum = 1;
                    while (path.Count != 0)//将path里面的点取出来，放在temp里面
                    {//path从栈顶-栈底的方向，路径是从终点-起点的顺序
                        Point p1 = path.Pop();
                        temp.Push(p1);
                    }
                    while (temp.Count != 0)
                    {//输出temp里面的路径，这样刚好是从起点到终点的顺序
                        Point p1 = temp.Pop();
                        path.Push(p1);//将路径放回path里面，因为后面还要回溯!!!
                        best.Push(p1);
                    }
                }
                if(bestWeight > weight)
                {
                    bestWeight=weight;
                    bestNum = sum;
                    best.Clear();
                    while (path.Count != 0)//将path里面的点取出来，放在temp里面
                    {//path从栈顶-栈底的方向，路径是从终点-起点的顺序
                        Point p1 = path.Pop();
                        temp.Push(p1);
                    }
                    while (temp.Count != 0)
                    {//输出temp里面的路径，这样刚好是从起点到终点的顺序
                        Point p1 = temp.Pop();
                        path.Push(p1);//将路径放回path里面，因为后面还要回溯!!!
                        best.Push(p1);
                    }
                }
                path.Pop();
                return;
            }
            if (nodes[a][b].up)
            {
                Point p = new Point();
                p.x = a;
                p.y = b;
                p.weight = nodes[a][b].upNum;
                path.Push(p);//让当前点进栈
                getSum(a, b + 1);
                
                path.Pop();
            }
            if (nodes[a][b].right)
            {
                Point p = new Point();
                p.x = a;
                p.y = b;
                p.weight = nodes[a][b].rightNum;
                path.Push(p);//让当前点进栈
                getSum(a + 1, b);
           
                path.Pop();
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sum = 0;
            nodes.Clear();
            chart1.Series.Clear();
            best.Clear();
            bestNum = 0;
            bestWeight = 0;
            m = int.Parse(textBox1.Text.Trim());
            n = int.Parse(textBox2.Text.Trim());
            getform();
            getSum(0, 0);
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            listBox1.Items.Add("******************最优路径" +bestNum + "******************");
            while (best.Count != 0)//将path里面的点取出来，放在temp里面
            {//path从栈顶-栈底的方向，路径是从终点-起点的顺序
                Point p1 = best.Pop();
                temp.Push(p1);
            }
            while (temp.Count != 0)
            {//输出temp里面的路径，这样刚好是从起点到终点的顺序
                Point p1 = temp.Pop();
                best.Push(p1);//将路径放回path里面，因为后面还要回溯!!!
                series.Points.AddXY(p1.x, p1.y);
                listBox1.Items.Add("(" + p1.x + "," + p1.y + ")");
            }
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series.Name = "最优路径" + bestNum + " 权重为" + bestWeight;
            chart1.Series.Add(series);
            listBox1.Items.Add("权重" + bestWeight);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
