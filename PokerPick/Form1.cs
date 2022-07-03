using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PokerPick
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PockerGame game = new PockerGame();
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            ShowState();
        }


        List<List<CheckBox>> boxes = new List<List<CheckBox>>();
        void ShowState()
        {
            //显示轮到哪个玩家
            labelUser.Text = game.UserTurn.ToString();

            //增加显示最后一行日志
            var record = game.Records.LastOrDefault();
            if (record != null)
            {
                var lines = richTextBox1.Lines.ToList();
                var left = string.Join(",", game.PockerStates);
                var line = string.Format("玩家{0}，捡起第{1}行{2}个火柴。剩余：{3}",record.UserTurn,record.RowNumber+1,record.PickCount,left);
                lines.Add(line);
                richTextBox1.Lines = lines.ToArray();
            }

            //画出CheckBox 代表剩余火柴
            if (boxes.Count > 0)
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < boxes[i].Count; j++)
                    {
                        this.Controls.Remove(boxes[i][j]);
                    }
                }
                boxes.Clear();
            }

            var y= 36;
            for(var i = 0; i < 3; i++)
            {
                var x = 56;
                var list = new List<CheckBox>();
                boxes.Add(list);
                for (var j = 0; j < game.PockerStates[i]; j++)
                {
                    var box = new CheckBox();
                    this.Controls.Add(box);
                    box.Location = new Point(x, y);
                    box.Width = 20;//默认100多，会挡住别的
                    box.Click += Box_CheckedChanged;  //增加类似五星打星功能，可做可不做
                    list.Add(box);
                    x += 25;
                }
                y += 30;
            }
        }

        private void Box_CheckedChanged(object sender, EventArgs e)
        {
            var box = sender as CheckBox;
            boxes.ForEach(line => {
                line.ForEach(p => p.Checked = false);
                if (line.Contains(box))
                {
                  var idx=  line.IndexOf(box);
                    for (var i = 0; i <= idx; i++)
                        line[i].Checked = true;
                }
            });
        }

        private void buttonPick_Click(object sender, EventArgs e)
        {
            var pickedRowCount = boxes.Where(p => p.Where(q => q.Checked).Count() > 0).Count();
            if (pickedRowCount > 1)
            {
                MessageBox.Show("只能捡起一行的火柴");
                return;
            }
            var row = -1;
            var pickCount = 0;
            for (var i = 0; i < 3; i++)
            {
                pickCount= boxes[i].Where(p => p.Checked).Count();
                if (pickCount > 0)
                {
                    row = i;
                    break;
                }
            }
            if (row == -1)
            {
                MessageBox.Show("至少要捡起一个火柴");
                return;
            }
            var stepCheck= game.Step(row, pickCount);
            if (stepCheck == false)
            {
                MessageBox.Show("游戏异常，不允许这样选择");
                return;
            }
            var end = game.CheckEnd();

           
            if (end)
            {
                MessageBox.Show(string.Format( "游戏结束，玩家{0}输了",labelUser.Text));
            }
            ShowState();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            game.ResetGame();
            ShowState();
        }

       
    }
}
