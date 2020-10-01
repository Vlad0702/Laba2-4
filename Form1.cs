using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MM
{
    public partial class Window : Form
    {

        // н и м - размеры соответсвующих массивов
        int n=-1, m=-1;
        quadrangle[] qmas; //сами массивы
        trapetion[] tmas;
        int n_current = 0; //указатель на текущую позицию в массиве
        int m_current = 0; //  -//-
        bool now_m = false; //сначала идет заполнение н массива, затем м. Как только н заполнится данный флаг станет тру и пойдет заполнение м
        public Window()
        {
          //конструктор. Инициализация компонентов
            InitializeComponent();
         
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            //данный метод вызывается когда происходит клик по кнопке "добавить"
            if (n_current == n)
            {
                //если текущая позиция в н массиве дошла до конца - ставим соответсвующий флаг и переходим к м массиву
                now_m = true;
            }

            double x1, x2, x3, x4, y1, y2, y3, y4;
            quadrangle d;
            trapetion t;
            try //сразу ловим все возможные ошибки
            {
                //считываем координаты из полей
                x1 = Convert.ToDouble(x1_f.Text);
                y1 = Convert.ToDouble(y1_f.Text);
                x2 = Convert.ToDouble(x2_f.Text);
                y2 = Convert.ToDouble(y2_f.Text);
                x3 = Convert.ToDouble(x3_f.Text);
                y3 = Convert.ToDouble(y3_f.Text);
                x4 = Convert.ToDouble(x4_f.Text);
                y4 = Convert.ToDouble(y4_f.Text);
                if (!now_m)
                {
                    //если время н массива
                    //создаем объект класса четырехугольник
                    d = new quadrangle(x1, y1, x2, y2, x3, y3, x4, y4);
                    if (!d.isExist())
                    {
                        //если с задаными координатами не существует четырех угольника
                        ///сообщение обо ошибке
                        ///и на второй круг
                        info_l.Text = "its insnt a quadrangale";
                        return;
                    }
                    ///если все ок
                    ///записываем этот объект в массив
                    ////и добавляем его в список записанных элементов
                    ///
                    qmas[n_current++] = d;
                    boxN.Items.Add(n_current + ": " + d.getCords());

                }
                else
                {
                    ///но если уже настало время м массива, то все тоже самое, но уже в м массив
                    ///
                    t = new trapetion(x1, y1, x2, y2, x3, y3, x4, y4);
                    if (!t.isExist())
                    {
                        info_l.Text = "its insnt a trapetion";
                        return;
                    }
                    tmas[m_current++] = t;
                    boxM.Items.Add(m_current + ": " + t.getCords());

                }
                info_l.Text = n_current + "/" + n + " quadrangale; " + m_current + "/" + m + " trapetion;"; //информация о процессе заполнения
                if (m_current+n_current == m+n)
                {
                 //как только заполним все
                 //выводим информацию мол все гуд, все хорошо
                 //и отключаем все кнопки которые были задействованы во избежании ошибок
                        info_l.Text = "good";
                        x1_f.Enabled = false;
                        y1_f.Enabled = false;
                        x2_f.Enabled = false;
                        y2_f.Enabled = false;
                        x3_f.Enabled = false;
                        y3_f.Enabled = false;
                        x4_f.Enabled = false;
                        y4_f.Enabled = false;
                        add_b.Enabled = false;

                    if (n != 0)
                    {
                        //так же находим максимальную площадь четырехугольников и записываем ее
                        int ma = Program.findMaxArea(qmas);
                        stat1.Text = "max space is " + qmas[ma].area() + " in quadranel#" + (ma + 1);
                    }
                    if (m != 0)
                    {
                        //ну и минимальную диагональ
                        int mi = Program.findMinD(tmas);
                        stat2.Text = "min d is " + Math.Min(tmas[mi].side(5), tmas[mi].side(6)) + " in trapetion#" + (mi + 1);
                    }
                    }

                //так же после каждой удачной записи увеличиваем заполнение прогресс бара
                progressBar1.Value++;
            }//конец блока с отлавливанием ошибок
            catch (Exception epa)
            {
                //если хоть одна была
                //например вместо цифр ввели буквы
                //или еще что-то
                //выводим информацию об ошибке
                info_l.Text = "Bad input, try again";
            }
        }

        private void boxN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //метод вызывается при выборе одного из элементов списка

            int ind = boxN.SelectedIndex; //получение координаты в этом списке
            if (ind >= n || ind < 0) return; //проверка диапазонов
            //вывод информации по элементу
            side1_l.Text = "s1 = " + qmas[ind].side(1);
            side2_l.Text = "s2 = " + qmas[ind].side(2);
            side3_l.Text = "s3 = " + qmas[ind].side(3);
            side4_l.Text = "s4 = " + qmas[ind].side(4);
            d1_l.Text = "d1 = " + qmas[ind].side(5);
            d2_l.Text = "d2 = " + qmas[ind].side(6);
            perim_l.Text = "p = " + qmas[ind].perim();
            space_l.Text = "s = " + qmas[ind].area();
        }

        private void boxM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //тоже самое, что и в методе выше
            ///только для второго списка
            int ind = boxM.SelectedIndex;
            if (ind >= m || ind < 0) return;

            side1_l.Text = "s1 = " + tmas[ind].side(1);
            side2_l.Text = "s2 = " + tmas[ind].side(2);
            side3_l.Text = "s3 = " + tmas[ind].side(3);
            side4_l.Text = "s4 = " + tmas[ind].side(4);
            d1_l.Text = "d1 = " + tmas[ind].side(5);
            d2_l.Text = "d2 = " + tmas[ind].side(6);
            perim_l.Text = "p = " + tmas[ind].perim();
            space_l.Text = "s = " + tmas[ind].area();
        }

       

        private void save_b_Click(object sender, EventArgs e)
        {
            //сохранение файла
            try
            {
                if (n_current + m_current != n + m)
                {
                    //если таблицы еще не заполнены - то и зачем сохранять

                    info_l.Text = "finish fill all position, and only then save";
                    return;
                }
                //устанавливаем фильтры
                saveFileDialog1.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
                saveFileDialog1.ShowDialog(); //вызываем окно
                Stream outs;
                outs = saveFileDialog1.OpenFile(); //записываем поток вывода
                BinaryWriter bw = new BinaryWriter(outs); 
                //структура файла следЖ
                //n;m;x1;y1;x2;y2;x3;y3;x4;y4;  и так дажее для каждого элемента
                bw.Write(n);
                bw.Write(m);
                for (int i = 0; i != n; i++)
                {
                    bw.Write(qmas[i].x1);
                    bw.Write(qmas[i].y1);
                    bw.Write(qmas[i].x2);
                    bw.Write(qmas[i].y2);
                    bw.Write(qmas[i].x3);
                    bw.Write(qmas[i].y3);
                    bw.Write(qmas[i].x4);
                    bw.Write(qmas[i].y4);
                }
                for (int i = 0; i != m; i++)
                {
                    bw.Write(tmas[i].x1);
                    bw.Write(tmas[i].y1);
                    bw.Write(tmas[i].x2);
                    bw.Write(tmas[i].y2);
                    bw.Write(tmas[i].x3);
                    bw.Write(tmas[i].y3);
                    bw.Write(tmas[i].x4);
                    bw.Write(tmas[i].y4);
                }
                outs.Flush();


                outs.Close();
            }
            catch (Exception epa)//так же ловим всевозожные ошибки
            {
                info_l.Text = "Some error in save";
            }
        }

        private void load_b_Click(object sender, EventArgs e)
        {
            //тоже самое и для загрузки
            //только загружать можно и не до конца заполнив таблицу
            try
            {
                openFileDialog1.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                Stream ins;
                ins = openFileDialog1.OpenFile();

                BinaryReader br = new BinaryReader(ins);
                n = br.ReadInt32();
                m = br.ReadInt32();
                n_current = n;
                m_current = m;
                qmas = new quadrangle[n];
                tmas = new trapetion[m];
                boxN.Items.Clear();
                boxM.Items.Clear();

                double x1, x2, x3, x4, y1, y2, y3, y4;
                for (int i = 0; i != n; i++)
                {
                    x1 = br.ReadDouble();
                    y1 = br.ReadDouble();

                    x2 = br.ReadDouble();
                    y2 = br.ReadDouble();

                    x3 = br.ReadDouble();
                    y3 = br.ReadDouble();

                    x4 = br.ReadDouble();
                    y4 = br.ReadDouble();
                    qmas[i] = new quadrangle(x1, y1, x2, y2, x3, y3, x4, y4);
                    boxN.Items.Add((i + 1) + ": " + qmas[i].getCords());
                }
                for (int i = 0; i != m; i++)
                {
                    x1 = br.ReadDouble();
                    y1 = br.ReadDouble();

                    x2 = br.ReadDouble();
                    y2 = br.ReadDouble();

                    x3 = br.ReadDouble();
                    y3 = br.ReadDouble();

                    x4 = br.ReadDouble();
                    y4 = br.ReadDouble();
                    tmas[i] = new trapetion(x1, y1, x2, y2, x3, y3, x4, y4);
                    boxM.Items.Add((i + 1) + ": " + tmas[i].getCords());
                }
                //после загрузки опять отключаем все кнопочки
                info_l.Text = "good";
                x1_f.Enabled = false;
                y1_f.Enabled = false;
                x2_f.Enabled = false;
                y2_f.Enabled = false;
                x3_f.Enabled = false;
                y3_f.Enabled = false;
                x4_f.Enabled = false;
                y4_f.Enabled = false;
                add_b.Enabled = false;
                boxN.Enabled = true;
                boxM.Enabled = true;

                //а так же обновляем инфомацию о минимальной диагонали и макс площади
                if (n != 0)
                {
                    int ma = Program.findMaxArea(qmas);
                    stat1.Text = "max space is " + qmas[ma].area() + " in quadranel#" + (ma + 1);
                }
                if (m != 0)
                {
                    int mi = Program.findMinD(tmas);
                    stat2.Text = "min d is " + Math.Min(tmas[mi].side(5), tmas[mi].side(6)) + " in trapetion#" + (mi + 1);
                }
            }catch(Exception epa) //не забываем ловить ошибки
            {
                info_l.Text = "Some error in load";
            }

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            //данный метод вызывается при указании м и н
            try
            {
                //записываем их
                n = Convert.ToInt32(n_in.Text);
                if (n < 0) throw new Exception(); //проверяем на корректность

                m = Convert.ToInt32(m_in.Text);
                if (m < 0) throw new Exception();

                if (n + m == 0) throw new Exception();

                //отключаем поля с ними
                n_in.Enabled = false;
                m_in.Enabled = false;

                info_l.Text = "n and m confirmed"; 
                //создаем новые массивы заданых размеров
                qmas = new quadrangle[n];
                tmas = new trapetion[m];
                progressBar1.Maximum = n + m; //устанавливаем диапазоны полоски загрузки
                //включаем кнопки для записи
                x1_f.Enabled = true;
                y1_f.Enabled = true;
                x2_f.Enabled = true;
                y2_f.Enabled = true;
                x3_f.Enabled = true;
                y3_f.Enabled = true;
                x4_f.Enabled = true;
                y4_f.Enabled = true;
                add_b.Enabled = true;
                boxN.Enabled = true;
                boxM.Enabled = true;
                button2.Enabled = false;

            }
            catch(Exception epa)
            {//так же ловим ошибки
                info_l.Text = "Bad input, try again";
            }

        }
    }
}
