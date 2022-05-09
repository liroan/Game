using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Model;

namespace Game
{
    public partial class Form1 : Form
    {
        private readonly Player player = new Player(1, 9, Direction.Up);
        private List<Tuple<Bot, BotView>> bots = new List<Tuple<Bot, BotView>>();
        private readonly GameField gameField = new GameField();
        private readonly PlayerView playerView;
        private readonly List<List<Vector>> Paths = new List<List<Vector>>();
        
        //private readonly BotView botView;
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(Form1_Paint);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            Timer timer = new Timer();
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = 50;
            timer.Start(); 
            Timer timer2 = new Timer();
            timer2.Tick += new EventHandler(OnTimer2);
            timer2.Interval = 2000;
            timer2.Start();
            //Paths.Add(gameField.BuildPath(new Vector(24, 0), 0, 15, 1));
            Paths.Add(gameField.BuildPath(new Vector(24, 0), 1, 14, 1));
            Paths.Add(gameField.BuildPath(new Vector(23, 0), 0, 14, 2));
            playerView = new PlayerView(player);
            //bots.Add(new Bot(23, 0, Direction.Up, 2, 0.5));
            //botView = new BotView(bots[0]);
            player.Path = gameField.BuildPath(new Vector((int)player.X, (int)player.Y), 24 + 1 - player.CurrentRoad, 0, 1);
            //bots[0].Path = gameField.BuildPath(new Vector((int)bots[0].X, (int)bots[0].Y), 0, 14, 2);
        }
        
        void OnTimer(object sender, EventArgs e)
        {
            player.CheckNextMove();
            foreach (var bot in bots)
            {
                CheckNextMove(bot);
            }
            /*bots[0].CheckNextMove();*/
            Invalidate();
        }

        void CheckNextMove(Tuple<Bot, BotView> bot)
        {
            var task = new Task(() => bot.Item1.CheckNextMove());
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
            /*bots.Add(new Bot(23, 0, Direction.Up, 1, 0.2));
            botView = new BotView(bots[0]);*/
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
            playerView.View(g);
            foreach (var bot in bots)
            {
                bot.Item2.View(g);
            }
        }
    }
}