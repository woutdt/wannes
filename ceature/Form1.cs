using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Configuration;

namespace creature
{
	public partial class Form1 : Form
	{

		Color color = Color.Black;
		Rectangle area2;
		Rectangle kogel;
		Rectangle kogel2;
		Bitmap creaturen;
		Bitmap backgroundimage;
		Graphics g;
		Rectangle area;
		Bitmap backgroud;
		Graphics scG;
		Rectangle[] shoot;
		Rectangle[] rects;
		Rectangle[] randomobstakels;
		Rectangle[] terrainrects;
		Rectangle[] walls;
		int collectedNumber = 0;
		int rectanglesinfeield = 150;
		int rectangles = 149;
		int heightwidht = 100;
		int areawidht = 100;
		int areaheight = 100;
		bool right;
		bool left;
		bool up;
		bool down;
		bool collide_r = false;
		bool collide_l = false;
		bool collide_t = false;
		bool collide_d = false;
		bool canched = false;
		int kakX = 0;
		int pipiY = 0;
		int inter = 0;
		public Form1()
		{

			InitializeComponent();




			creaturen = new Bitmap(@"C:\Users\Wannes\Documents\Visual Studio 2017\Projects\creature\ceature\fotos\pik.png");
			g = this.CreateGraphics();
			backgroud = new Bitmap(this.Width, this.Height);
			area = new Rectangle(this.Width/2, this.Height/2, 100, 150);
			scG = Graphics.FromImage(backgroud);
			terrainrects = new Rectangle[4000];
			maketerrain();
			walls = new Rectangle[10];
			makewalls();
			rects = new Rectangle[rectanglesinfeield];

			makerect();
			randomobstakels = new Rectangle[3000];

			kakX = this.Width * 2;
			pipiY = this.Height * 3;
			area2 = new Rectangle(0, 0, kakX, pipiY);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			timer.Start();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{

				if (!canched)
				{
					area = new Rectangle(this.Width / 2, this.Height / 2, areawidht, (areaheight/2) + areaheight);
					creaturen = new Bitmap(@"C:\Users\Wannes\Documents\Visual Studio 2017\Projects\creature\ceature\fotos\pik.png");


				}

				canched = true;
				up = true;
			}
			if (e.KeyCode == Keys.Down)
			{

				if (!canched)
				{
					area = new Rectangle(this.Width / 2, this.Height / 2, areawidht, (areaheight / 2) + areaheight);
					creaturen = new Bitmap(@"C:\Users\Wannes\Documents\Visual Studio 2017\Projects\creature\ceature\fotos\pik.png");
					creaturen.RotateFlip(RotateFlipType.Rotate180FlipNone);

				}
				canched = true;
				down = true;
			}
			if (e.KeyCode == Keys.Left)
			{

				if (!canched)
				{
					area = new Rectangle(this.Width / 2, this.Height / 2, (areawidht/2) + areawidht, areaheight);
					creaturen = new Bitmap(@"C:\Users\Wannes\Documents\Visual Studio 2017\Projects\creature\ceature\fotos\pik.png");
					creaturen.RotateFlip(RotateFlipType.Rotate270FlipNone);

				}
				canched = true;
				left = true;

			}
			if (e.KeyCode == Keys.Right)
			{

				if (!canched)
				{
					area = new Rectangle(this.Width / 2, this.Height / 2, areawidht/2 +areawidht, areaheight);
					creaturen = new Bitmap(@"C:\Users\Wannes\Documents\Visual Studio 2017\Projects\creature\ceature\fotos\pik.png");
					creaturen.RotateFlip(RotateFlipType.Rotate90FlipNone);

				}
				canched = true;
				right = true;
				
			}
			if (e.KeyCode == Keys.Space)
			{

				
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Up)
			{

				
				canched = false;
				up = false;
			}
			if (e.KeyCode == Keys.Down)
			{

				canched = false;
				down = false;
			}
			if (e.KeyCode == Keys.Left)
			{

				canched = false;
				left = false;

			}
			if (e.KeyCode == Keys.Right)
			{

				canched = false;
				right = false;
			}

			if (e.KeyCode == Keys.M)
			{
				openFileDialog1.ShowDialog();
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (up) { MoveRect(Direction.up); }
			if (down) { MoveRect(Direction.down); }
			if (left) { MoveRect(Direction.left); }
			if (right) { MoveRect(Direction.right); }
			g.DrawImage(Draw(), new Point(0, 0));

			for (int i = 0; i < rects.Length; i++)
			{
				if (area.IntersectsWith(rects[i]))
				{
					collectedNumber++;
					rects[i] = new Rectangle(-100, -100, 1, 1);
					rectanglesinfeield = rectanglesinfeield - 1;
					areawidht++;
					areaheight++;
				}
			}

		}
		public enum Direction { up, right, down, left };

		public void MoveRect(Direction d)
		{

			for (int i = 0; i < rects.Length; i++)
			{
				if (d == Direction.up && !collide_d) { rects[i].Y += 10; }
				if (d == Direction.down && !collide_t) { rects[i].Y -= 10; }
				if (d == Direction.left && !collide_r) { rects[i].X += 10; }
				if (d == Direction.right && !collide_l) { rects[i].X -= 10; }
			}
			for (int i = 0; i < terrainrects.Length; i++)
			{
				if (d == Direction.up && !collide_d) { terrainrects[i].Y += 10; }
				if (d == Direction.down && !collide_t) { terrainrects[i].Y -= 10; }
				if (d == Direction.left && !collide_r) { terrainrects[i].X += 10; }
				if (d == Direction.right && !collide_l) { terrainrects[i].X -= 10; }

			}
			for (int i = 0; i < walls.Length; i++)
			{
				if (d == Direction.up && !collide_d) { walls[i].Y += 10; }
				if (d == Direction.down && !collide_t) { walls[i].Y -= 10; }
				if (d == Direction.left && !collide_r) { walls[i].X += 10; }
				if (d == Direction.right && !collide_l) { walls[i].X -= 10; }

			}
			for (int i = 0; i < randomobstakels.Length; i++)
			{
				if (d == Direction.up && !collide_d) { randomobstakels[i].Y += 10; }
				if (d == Direction.down && !collide_t) { randomobstakels[i].Y -= 10; }
				if (d == Direction.left && !collide_r) { randomobstakels[i].X += 10; }
				if (d == Direction.right && !collide_l) { randomobstakels[i].X -= 10; }

			}
			if (d == Direction.up && !collide_d) { area2.Y += 10; }
			if (d == Direction.down && !collide_t) { area2.Y -= 10; }
			if (d == Direction.left && !collide_r) { area2.X += 10; }
			if (d == Direction.right && !collide_l) { area2.X -= 10; }

			if (collide_d && collide_l && collide_r && collide_t)
			{
				for (int i = 0; i < randomobstakels.Length; i++)
				{
					if (randomobstakels[i].IntersectsWith(area))
					{
						randomobstakels[i] = new Rectangle(-100, -100, 0, 0);

					}
					else
					{

					}
				}
			}



		}
		public void makewalls()
		{
			walls[0] = new Rectangle(-60, -40, this.Width * 2 + 60, 40);
			walls[1] = new Rectangle(-40, this.Height * 3 - 40, this.Width * 2 + 100, 40);
			walls[2] = new Rectangle(-40, -60, 40, this.Height * 3 - 10);
			walls[3] = new Rectangle(this.Width * 2 + 40, -40, 40, this.Height * 3 + 60);
		}
		public void maketerrain()
		{
			int rectpos = 0;

			for (int x = 0; x < this.Width * 2; x += 40)
			{
				for (int y = 0; y < this.Width * 2; y += 40)
				{
					terrainrects[rectpos++] = new Rectangle(x, y, 40, 40);
					
				}
			}
		}

		private void movetocursor()
		{

		}


		public void makeobstakels()
		{
			
			Random r = new Random();

			for (int y = 0; y < randomobstakels.Length; y += 40)
			{
				randomobstakels[y] = new Rectangle(r.Next(0,this.Width*2), r.Next(0,this.Height*3-10), r.Next(40, 120), r.Next(40, 120));
			}
		}

		public Rectangle[] makerect()
		{
			Random r = new Random();

			for(int i =0; i<rects.Length; i++)
			{
				rects[i] = new Rectangle(r.Next(0, this.Width*2), r.Next(0, this.Height*3 - 60),10,10);
			}
			return rects;
		}
		public Rectangle[] makerect2()
		{
			Random r = new Random();

			for (int i = 0; i < rects.Length; i++)
			{
				rects[i] = new Rectangle(r.Next(0, this.Width * 2), r.Next(0, this.Height * 3 - 60), 10, 10);
			}
			return rects;
		}
		public Bitmap Draw()
		{

			scG.Clear(Color.FromArgb(255, Color.White));

			if (backgroundimage != null)
			{
				scG.DrawImage(backgroundimage, area2);
			}
			for (int i = 0; i < rects.Length; i++)
			{
				scG.FillEllipse(Brushes.Gold, rects[i]);

			}
			scG.FillEllipse(Brushes.Gold, rects[rectangles]);
			scG.DrawRectangles(new Pen(Brushes.LightGray), terrainrects);
			scG.DrawImage( creaturen, area);
			scG.FillRectangles(Brushes.Transparent, walls);
			scG.FillEllipse(Brushes.Purple, kogel);
			scG.FillRectangles(Brushes.Black, randomobstakels); 
			scG.DrawString(collectedNumber.ToString(), new Font("Arial", 30), Brushes.Black, new PointF(10, 10));
			return backgroud;


		}

		private void collision_Tick(object sender, EventArgs e)
		{
			
			 collide_r = false;
			 collide_l = false;
			 collide_t = false;
			 collide_d = false;
			for (int i = 0; i < walls.Length; i++)
			{

				if (walls[i] != null)
				{
					
					if (walls[i].IntersectsWith(area))
					{

						if (walls[i].Left < area.Right && area.X < walls[i].Left)
						{
							collide_l = true;
						}

						if (walls[i].Right > area.Left && walls[i].Right < area.X + area.Width)
						{
							collide_r = true;
						}

						if (walls[i].Top < area.Y + area.Height && area.Y < walls[i].Top)
						{
							collide_t = true;
						}

						if (walls[i].Bottom > area.Y && area.Y + area.Height > walls[i].Bottom )
						{
							collide_d = true;
						}

					}
				}
			}
			for (int i = 0; i < randomobstakels.Length; i++)
			{

				if (randomobstakels[i] != null)
				{

					if (randomobstakels[i].IntersectsWith(area))
					{
						if (area.Width < randomobstakels[i].Width && area.Height < randomobstakels[i].Height)
						{
							if (randomobstakels[i].Left < area.Right && area.X < randomobstakels[i].Left)
							{
								collide_l = true;
							}

							if (randomobstakels[i].Right > area.Left && randomobstakels[i].Right < area.X + area.Width)
							{
								collide_r = true;
							}

							if (randomobstakels[i].Top < area.Y + area.Height && area.Y < randomobstakels[i].Top)
							{
								collide_t = true;
							}

							if (randomobstakels[i].Bottom > area.Y && area.Y + area.Height > randomobstakels[i].Bottom)
							{
								collide_d = true;
							}
						}
						else
						{
							if (collectedNumber < 10)
							{
								if (randomobstakels[i].Left < area.Right && area.X < randomobstakels[i].Left)
								{
									collide_l = true;
								}

								if (randomobstakels[i].Right > area.Left && randomobstakels[i].Right < area.X + area.Width)
								{
									collide_r = true;
								}

								if (randomobstakels[i].Top < area.Y + area.Height && area.Y < randomobstakels[i].Top)
								{
									collide_t = true;
								}

								if (randomobstakels[i].Bottom > area.Y && area.Y + area.Height > randomobstakels[i].Bottom)
								{
									collide_d = true;
								}
							}
							else
							{
								collectedNumber = collectedNumber - 10;
								randomobstakels[i] = new Rectangle(-100, -100, 0, 0);
								area.Height = area.Height - 10;
								area.Width = area.Width - 10;
							}

						}

					}
				}
			}
		}

		private void bolspawn_Tick(object sender, EventArgs e)
		{
			
		}

		private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

			backgroundimage = new Bitmap(openFileDialog1.FileName);
		}



		private void rectinfield_Tick(object sender, EventArgs e)
		{

			}
	}
}
