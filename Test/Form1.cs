using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random             random;
        private int        randI;
        private int        randF;
        private int        rezult = 0;
        private int        count = 9;
        private string     broker1;
        private bool       met_cond;
        private bool       u = false;
        private string[]   brokQuest = new string[10];
        private string[]   brokQuest_massAnsw = new string[10];
        private string[]   brokAnsw = new string[4];
        private string[][] AllAnsw = new string[10][];

        private void InitializationAnsw() //Метод иницилизирующий массив массивов(зубчатый) с ответами
        {
            AllAnsw[0] = new string[4];
            AllAnsw[1] = new string[4];
            AllAnsw[2] = new string[4];
            AllAnsw[3] = new string[4];
            AllAnsw[4] = new string[4];
            AllAnsw[5] = new string[4];
            AllAnsw[6] = new string[4];
            AllAnsw[7] = new string[4];
            AllAnsw[8] = new string[4];
            AllAnsw[9] = new string[4];
        }
        static void RemoveAt(ref string[] array, int index) //Метод удаляющий элемент массива, чтобы вопросы и ответы не повторялись 
        {
            string[] new_array = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
                new_array[i] = array[i];

            for (int i = index + 1; i < array.Length; i++)
                new_array[i - 1] = array[i];

            array = new_array;
        }
        private string[] SearchArrAnsw(int i, string[] Quest, string[] array)//Метод ищущий нужный массив ответов для заданного вопроса
        {
            string h = Quest[i]; //Присваивания вопроса выведеного в лейбл
            string[] broker = new string[4];
            for (int y = 0; y < array.Length; y++)
            {
                if (h == array[y])
                {
                    broker = AllAnsw[y];
                    break;
                }
            }
            return broker;
        }
        static string[] ReadQuest()//Метод считывающий вопросы из документа в массив
        {
            StreamReader reader = new StreamReader(@"..\Questions\Test.txt");
            string[] brokerQuest = new string[10];
            reader.Read();
            reader.Read();
            brokerQuest[0] = reader.ReadLine();
            for (int n = 1; n <= 9; n++)
            {
                for (int m = 0; m < 5; m++)
                {
                    reader.Read();
                    reader.Read();
                    brokerQuest[n] = reader.ReadLine();
                }
            }
            reader.Close();
            return brokerQuest;
        }
        private void ReadAnsw()//Метод считывающий ответы к вопросам в массивы
        {
            StreamReader reader = new StreamReader(@"..\Questions\Test.txt");
            reader.ReadLine();

            for (int h = 0; h < 10; h++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    AllAnsw[h][i] = reader.ReadLine();
                }
                reader.ReadLine();
            }
            reader.Close();
        }
        private void button1_Click(object sender, EventArgs e)//Обработчик нажатия на кнопу 1
        {
           
            if (count == 9)
            {
                InitializationAnsw();
                brokQuest = ReadQuest();
                brokQuest_massAnsw = brokQuest;
                ReadAnsw();
                rezult = 0;
                button2.Visible = false;
                
            }
            //Данная часть кода нужна для того, чтобы метод radioButton1_Click 
            //не выплнялся 2 раза при нажатии на radiobutton
            met_cond = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            met_cond = true;
            //Проверка на наличие вопросов с помощь проверки счетчика
            if (count >= 0)
            {
                //Работа с формой
                this.Size = new Size(820, 453);
                label1.Text = "";
                label2.Text = "Вопрос";
                label4.Text = "Варианты ответа";
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                button1.Text = "Далее";
                //Выбор вопроса, нахождение массива ответов
                //и запоминание правильного ответа
                randI = random.Next(0, count);
                label3.Text = brokQuest[randI];
                count--;
                brokAnsw = SearchArrAnsw(randI, brokQuest, brokQuest_massAnsw);
                broker1 = brokAnsw[0];
                //Вывод ответов в radiobutton
                randF = random.Next(0, 3);
                radioButton1.Text = brokAnsw[randF];
                RemoveAt(ref brokAnsw, randF);
                randF = random.Next(0, 2);
                radioButton2.Text = brokAnsw[randF];
                RemoveAt(ref brokAnsw, randF);
                randF = random.Next(0, 1);
                radioButton3.Text = brokAnsw[randF];
                RemoveAt(ref brokAnsw, randF);
                randF = 0;
                radioButton4.Text = brokAnsw[randF];
                //Удалиение использованного элемента из массива вопросов
                RemoveAt(ref brokQuest, randI);
                //Обнуление переменной для метода radioButton1_Click
                if (u != false)
                    u = false;
            }
            else if (count == -1)
            {
                //Изменение формы после прохождения всех вопросов
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                button2.Visible = true;
                label2.Text = "";
                label4.Text = "";
                button1.Text = "Попробовать еще";
                label3.Text = Convert.ToString($"Ваш балл: {rezult} из 10 ");
                count = 9;
            }
        }
        private void radioButton1_Click(object sender, EventArgs e) //Обработчик выбора ответа на вопрос
        {
            RadioButton rBut = (RadioButton)sender;
            if (broker1 == rBut.Text & met_cond == true & u == false)
            {
                rezult++;
                u = true;
            }
            if (broker1 != rBut.Text & met_cond == true & u == true)
            {
                rezult--;
                u = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)//Обработчик нажатия на кнопку 2
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            random = new Random();
        }
    }
}

