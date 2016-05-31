﻿using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.MetaDetector.Controls
{
    /// <summary>
    /// Interaction logic for OpDeckWindow.xaml
    /// </summary>
    public partial class OpDeckWindow
    {
        public OpDeckWindow()
        {
            InitializeComponent();

            lbDecks.DisplayMemberPath = "GetClass";
        }

        public void clearLists()
        {
            lvDeckList.ItemsSource = null;
            lbDecks.Items.Clear();
        }

        public void resetWindow(List<Deck> metaDecks)
        {
            lvDeckList.ItemsSource = null;
            lbDecks.Items.Clear();
            /*foreach (Deck deck in metaDecks.OrderBy(d => d.Class))
            {
                deck.Name = "Meta Deck";
                lbDecks.Items.Add(deck);
            }*/
            tbInformation.Text = "Waiting...";
            tbInformation.Foreground = Brushes.White;
            tbMetaRank.Text = "";            
        }

        public void updateCardList(Deck deck)
        {
            lvDeckList.ItemsSource = deck.Cards;
            Hearthstone_Deck_Tracker.Helper.SortCardCollection(lvDeckList.Items, false);
        }

        public void updateCardsCount(int count)
        {
            tbCardsPlayed.Text = "Cards Matched: " + count.ToString();
        }

        public void updateDeckList(List<Deck> metaDecks)
        {
            try
            {
                if (metaDecks.Count > 0)
                {
                    lbDecks.Items.Clear();

                    foreach (Deck deck in metaDecks.OrderByDescending(x => Convert.ToInt32(x.Note)).ThenBy(x=>x.Class))
                    {
                        lbDecks.Items.Add(deck);
                    }

                    if(lbDecks.Items.Count > 0)
                        lbDecks.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
        }

        public Deck getSelectedDeck()
        {
            return (Deck)lbDecks.SelectedItem;
        }

        public void setSelectedDeck(Deck selectedDeck)
        {
            //lbDecks.SelectedItem = selectedDeck;
        }

        public void updateText(string message, Brush color)
        {
            tbInformation.Text = message;
            tbInformation.Foreground = color;
        }

        private void DeckWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lbDecks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbDecks.SelectedItem != null)
            {
                lvDeckList.ItemsSource = ((Deck)lbDecks.SelectedItem).Cards;
                Hearthstone_Deck_Tracker.Helper.SortCardCollection(lvDeckList.Items, false);

                tbMetaRank.Text = "Meta Rank: " + ((Deck)lbDecks.SelectedItem).Note;
            }
        }

        private void lbDecks_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
