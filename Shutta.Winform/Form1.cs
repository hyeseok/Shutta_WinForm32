using Shutta.Winform.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shutta.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            players.Add(new Player(0));
            players.Add(new Player(1));

            moneyLabels[0] = lblMoney0;
            moneyLabels[1] = lblMoney1;

            cardImages[0, 0] = ptb00;
            cardImages[0, 1] = ptb01;
            cardImages[1, 0] = ptb10;
            cardImages[1, 1] = ptb11;

            rdbBasic.Checked = true;
        }

        Dealer dealer = new Dealer();
        Board board = new Board();
        List<Player> players = new List<Player>();
        Scorer scorer = new BasicScorer();

        private Label[] moneyLabels = new Label[2];

        private PictureBox[,] cardImages = new PictureBox[2, 2];

        private void btnRound_Click(object sender, EventArgs e)
        {
            // 딜러가 카드를 섞음
            dealer.Shuffle();

            // 판돈 내기
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Money -= 100;
                board.Money += 100;
            }

            // 각 플레이어에 카드를 2장씩 준다.
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Cards.Clear();

                for (int j = 0; j < 2; j++)
                {
                    Card card = dealer.GetCard();
                    players[i].TakeCard(card);
                }
            }

            // 승자를 찾는다
            Player winner = scorer.GetWinner(players[0], players[1]);

            if (winner != null)
            {
                // 판돈을 승자에게 준다.
                winner.Money += board.Money;
                board.Money = 0;
            }

            // 플레이어가 가진 카드와 소지금, 승자를 출력한다.
            PrintRoundResult(players, winner);

            if (CanGoRound(players) == false)
                btnRound.Enabled = false;
        }

        private void PrintRoundResult(List<Player> players, Player winner)
        {
            for (int i = 0; i < players.Count; i++)
            {
                moneyLabels[i].Text = players[i].Money.ToString();

                for (int j = 0; j < 2; j++)
                {
                    Card card = players[i].Cards[j];
                    Image image = FindImage(card);
                    cardImages[i, j].Image = image;
                }
            }

            if (winner.No == 0)
            {
                grbPlayer0.BackColor = Color.Yellow;
                grbPlayer1.BackColor = Color.Transparent;
            }
            else
            {
                grbPlayer0.BackColor = Color.Transparent;
                grbPlayer1.BackColor = Color.Yellow;
            }


            Text = $"승자는 {winner.No}.";
        }

        bool CanGoRound(List<Player> players)
        {
            if (players[0].Money > 0 && players[1].Money > 0)
                return true;

            return false;
        }

        private Image FindImage(Card card)
        {
            if (card.IsKwang)
            {
                switch (card.Number)
                {
                    case 1:
                        return Resources.C1K;
                    case 3:
                        return Resources.C3K;
                    case 8:
                        return Resources.C8K;
                    default:
                        throw new Exception();
                }
            }
            else
            {
                switch (card.Number)
                {
                    case 1:
                        return Resources.C1;
                    case 2:
                        return Resources.C2;
                    case 3:
                        return Resources.C3;
                    case 4:
                        return Resources.C4;
                    case 5:
                        return Resources.C5;
                    case 6:
                        return Resources.C6;
                    case 7:
                        return Resources.C7;
                    case 8:
                        return Resources.C8;
                    case 9:
                        return Resources.C9;
                    case 10:
                        return Resources.C10;
                    default:
                        throw new Exception();
                }
            }
        }

        private void rdbSimple_CheckedChanged(object sender, EventArgs e)
        {
            scorer = new SimpleScorer();
        }

        private void rdbBasic_CheckedChanged(object sender, EventArgs e)
        {
            scorer = new BasicScorer();
        }
    }
}
