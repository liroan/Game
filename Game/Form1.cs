using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Model;
using Timer = System.Windows.Forms.Timer;

namespace Game
{
    public partial class Form1 : Form
    {
        private Player player = new Player(1, 9, Direction.Up);
        private List<Tuple<Bot, BotView>> bots = new List<Tuple<Bot, BotView>>();
        private readonly GameField gameField = new GameField();
        private readonly PlayerView playerView;
        private readonly List<List<Vector>> Paths = new List<List<Vector>>();
        private Form form2 = new EndForm();
        private Timer timer = new Timer();
        private Timer timer2 = new Timer();
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(Form1_Paint);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = 50;
            timer.Start();
            timer2.Tick += new EventHandler(OnTimer2);
            timer2.Interval = 2000;
            timer2.Start();
            Paths.Add(gameField.BuildPath(new Vector(24, 0), 1, 14, 1));
            Paths.Add(gameField.BuildPath(new Vector(23, 0), 0, 14, 2));
            playerView = new PlayerView(player);
            player.Path = gameField.BuildPath(new Vector((int)player.X, (int)player.Y), 24 + 1 - player.CurrentRoad, 0, 1);
        }

        void endGame()
        {
            Paint -= new PaintEventHandler(Form1_Paint);
            KeyDown -= new KeyEventHandler(Form1_KeyDown);
            timer.Tick -= new EventHandler(OnTimer);
            timer2.Tick -= new EventHandler(OnTimer2);
        }
        void OnTimer(object sender, EventArgs e)
        {
            if (player != null && !player.IsArrived) 
                player.CheckNextMove();
            else
            {
                form2.Show();
                form2.Activate();
                endGame();
                Hide();
                player = null;
            }

            for (var i = 0; i < bots.Count; i++)
            {
                CheckNextMove(bots[i], i, bots);
            }
            Invalidate();
        }

        void CheckNextMove(Tuple<Bot, BotView> bot, int i, List<Tuple<Bot, BotView>> bots)
        {
            var task = new Task(() =>
            {
                if (bot != null && !bot.Item1.IsArrived)
                {
                    if (bot.Item1.CheckCollision(player))
                        Application.Exit();
                    bot.Item1.CheckNextMove();
                }
                else
                {
                    bots[i] = null;
                    Console.WriteLine("lol");
                }
            });
            task.Start();
        }
        
        void OnTimer2(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int road = rnd.Next(0,2);
            var bot = new Bot(24 - road, 0, Direction.Up, road + 1, 0.5);
            var botView = new BotView(bot);
            bot.Path = Paths[road];
            bots.Add(Tuple.Create<Bot, BotView>(bot, botView));
            Invalidate();
        }
        
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                {
                    var x = (int) Math.Round(player.X);
                    var y = (int)Math.Round(player.Y) - 1;
                    if (gameField.IsCanRearrange(x, y, player.CurrentRoad))
                    {
                        player.SwapPath(x, y, Direction.Up, gameField);
                    }
                    break;
                }
                case Keys.Down:
                {
                    var x = (int) Math.Round(player.X);
                    var y = (int)Math.Round(player.Y) + 1;
                    if (gameField.IsCanRearrange(x, y, player.CurrentRoad))
                    {
                        player.SwapPath(x, y, Direction.Down, gameField);
                    }
                    break;
                }
                case Keys.Right:
                {
                    var x = (int)Math.Round(player.X) + 1;
                    var y = (int)Math.Round(player.Y);
                    if (gameField.IsCanRearrange(x, y, player.CurrentRoad))
                    {
                        player.SwapPath(x, y, Direction.Right, gameField);
                    }
                    break;
                }
                case Keys.Left:
                {
                    var x = (int)Math.Round(player.X) - 1;
                    var y = (int)Math.Round(player.Y);
                    if (gameField.IsCanRearrange(x, y, player.CurrentRoad))
                    {
                        player.SwapPath(x, y, Direction.Left, gameField);
                    }
                    break;
                }
                    
            }
            Invalidate();
        }
        
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (var i = 0; i < gameField.Field.GetLength(0); i++)
            {
                for (var j = 0; j < gameField.Field.GetLength(1); j++)
                {
                    if (gameField.Field[i, j].IsRoad)
                        g.FillRectangle(Brushes.DimGray, new Rectangle(gameField.Field[i, j].X * gameField.Field[i, j].Size,
                            gameField.Field[i, j].Y * gameField.Field[i, j].Size, 
                            gameField.Field[i, j].Size, gameField.Field[i, j].Size));
                    else
                    {
                        g.FillRectangle(Brushes.GreenYellow, new Rectangle(gameField.Field[i, j].X 
                                                                           * gameField.Field[i, j].Size,
                            gameField.Field[i, j].Y * gameField.Field[i, j].Size, 
                            gameField.Field[i, j].Size, gameField.Field[i, j].Size));
                    }
                }
            }
            if (player != null)
                playerView.View(g);
            foreach (var bot in bots)
            {
                if (bot != null)
                    bot.Item2.View(g);
            }
        }
    }
    public class PersonView<T> where T: IPerson
    {
        public T person;
        public string nameImg;
        public PersonView(T person, string nameImg)
        {
            this.person = person;
            this.nameImg = nameImg;
        }

        public void View(Graphics g)
        {
            var numberImage = ((int)person.Dir).ToString();
            Console.WriteLine("..\\Game\\img\\pngwing" + numberImage + ".png");
            
            Image newImage = Image.FromFile("../../../img/" + nameImg + numberImage + ".png");
            g.DrawImage(newImage, new Rectangle((int)(person.X * person.Size),
                (int)(person.Y * person.Size), person.Size, person.Size));
        }
    }
    public class BotView:PersonView<Bot>
    {
        public BotView(Bot person, string nameImg="bot"): base(person, nameImg)
        {
            this.person = person;
        }
        
    }
    public class PlayerView:PersonView<Player>
    {
        public PlayerView(Player person, string nameImg = "pngwing"): base(person, nameImg)
        {
            this.person = person;
        }
    }
}