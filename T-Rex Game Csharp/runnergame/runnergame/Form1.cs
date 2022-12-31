using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace runnergame
{
    public partial class Form1 : Form
    {


        bool jump = false;
        int jumpspeed;
        int force = 12;
        int score = 0;
        int obstaclespeed = 10;
        
        Random rand = new Random();
        int currentposition;
        bool isGameOver = false;


        public Form1()
        {
            InitializeComponent();

            GamePlayAgain();
        }


        //This is main game where all instruction of game will be writtn here.
        //about jump of character and translate animation of objects (cars) and score and
        //restart game also call here.
        private void MyGameInterval(object sender, EventArgs e)
        {

            sonic.Top += jumpspeed;

            txtScore.Text = "Score: " + score;

            if (jump == true && force < 0)
            {

                jump = false;
            }

            if (jump == true)
            {

                jumpspeed = -35;
                force -= 10;
            }
            else
            {
                jumpspeed = 3;
            }

            if (sonic.Top > 303 && jump == false)
            {
                force = 52;
                sonic.Top = 304;
                jumpspeed = 0;
            }


            // this code is used to pass the obstacle from character and
            // check that its pass from it and change score 

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "cars")
                {

                    x.Left -= obstaclespeed; //pass cars(obstacles) from right to left 

                    if (x.Left < -100)
                    {
                        // pass cars  again and again
                        x.Left = this.ClientSize.Width + rand.Next(500, 600) + (x.Width * 15);
                        score++;
                    }

                    //If hit with obstacle game will over and score will stop

                    if (sonic.Bounds.IntersectsWith(x.Bounds))
                    {

                        gameTimer.Stop();
                        sonic.Image = Properties.Resources.deadsonic22;

                        txtScore.Text = "game Over: Press R to restat the game";
                        isGameOver = true;
                    }


                }
            }



        }

        private void downKey(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space && jump == false)
            {

                jump = true;
            }

        }

        private void upKey(object sender, KeyEventArgs e)
        {
            if (jump == true)
            {
                jump = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GamePlayAgain();
            }
        }


        // when we need to reset the game we call this function to play again  
        private void GamePlayAgain()
        {

            force = 12;
            jumpspeed = 0;
            jump = false;
            score = 0;
            obstaclespeed = 10;
            txtScore.Text = "Score: " + score;
            sonic.Image = Properties.Resources.sonic_icegif_15;
            isGameOver = false;
            sonic.Top = 304;



            //Setting the position of cars (Obstacles) out
            //    of the frame so when game start it will move farwad to the charachter

            //At this stage this is not accurate we will check after translate from right to left these car
            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "cars")
                {

                    currentposition = this.ClientSize.Width + rand.Next(200, 400) + (x.Width * 45);

                    x.Left = currentposition;

                }
            }
            gameTimer.Start();


        }

        private void Form1_Load(object sender, EventArgs e)
        {


            /*
            pictureBox3.BackColor = Color.Transparent;

            pictureBox4.BackColor = Color.Transparent;
            sonic.BackColor = Color.Transparent;

            pictureBox1.BackColor = Color.Transparent;
            pictureBox7.BackColor = Color.Transparent;
            pictureBox8.BackColor = Color.Transparent;
            pictureBox6.BackColor = Color.Transparent;
            */

        }
    }
}
