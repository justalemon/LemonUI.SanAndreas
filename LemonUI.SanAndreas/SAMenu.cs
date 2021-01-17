#if !FIVEM
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;
#endif
using System;
using System.Collections.Generic;
using LemonUI.Menus;
using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.SanAndreas
{
    /// <summary>
    /// A GTA San Andreas Inspired menu.
    /// </summary>
    public class SAMenu : IContainer<SAItem>
    {
        #region Fields

        private bool visible;
        private int index = 0;
        private readonly ScaledText title = new ScaledText(PointF.Empty, "");
        private readonly ScaledRectangle background = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(125, 0, 0, 0)
        };
        private readonly List<SAItem> items = new List<SAItem>();

        #endregion

        #region Properties

        /// <summary>
        /// If this menu is visible or not.
        /// </summary>
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible == value)
                {
                    return;
                }
                if (value)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
        }
        /// <summary>
        /// The index of the currently selected item.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (items.Count == 0 || index >= items.Count)
                {
                    return -1;
                }
                return index;
            }
            set
            {
                if (items.Count == 0)
                {
                    throw new InvalidOperationException("The menu does not has any items.");
                }
                else if (value >= items.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Index is over {items.Count - 1}.");
                }
                index = value;
            }
        }
        /// <summary>
        /// The item currently selected on the menu.
        /// </summary>
        public SAItem SelectedItem
        {
            get
            {
                if (items.Count == 0 || index >= items.Count)
                {
                    return null;
                }
                return items[SelectedIndex];
            }
            set
            {
                if (!items.Contains(value))
                {
                    throw new InvalidOperationException("Item is not part of the Menu.");
                }
                SelectedIndex = items.IndexOf(value);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the menu is being opened.
        /// </summary>
        public event CancelEventHandler Opening;
        /// <summary>
        /// Event triggered when the menu is opened and shown to the user.
        /// </summary>
        public event EventHandler Shown;
        /// <summary>
        /// Event triggered when the menu starts closing.
        /// </summary>
        public event CancelEventHandler Closing;
        /// <summary>
        /// Event triggered when the menu finishes closing.
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// Event triggered when the index has been changed.
        /// </summary>
        public event SelectedEventHandler SelectedIndexChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new menu with no title.
        /// </summary>
        public SAMenu() => Recalculate();
        /// <summary>
        /// Creates a new menu with the specified title.
        /// </summary>
        /// <param name="title">The title to use.</param>
        public SAMenu(string title)
        {
            this.title.Text = title;
        }

        #endregion

        #region Functions

        private void TriggerSelected()
        {
            // Get the currently selected item
            SAItem item = SelectedItem;
            // If is null or the menu is closed, return
            if (item == null || !Visible)
            {
                return;
            }
            // Otherwise, trigger the selected event for this menu
            SelectedEventArgs args = new SelectedEventArgs(index, index);
            item.OnSelected(this, args);
            SelectedIndexChanged?.Invoke(this, args);
        }
        private void Open()
        {
            // Check if we need to cancel the menu opening and return if we do
            CancelEventArgs args = new CancelEventArgs();
            Opening?.Invoke(this, args);
            if (args.Cancel)
            {
                return;
            }
            // Mark the menu as visible
            visible = true;
            // And trigger the shown events for the menu and selected item
            Shown?.Invoke(this, EventArgs.Empty);
            TriggerSelected();
        }
        private void Close()
        {
            // Check if we need to cancel the menu closure and return if we do
            CancelEventArgs args = new CancelEventArgs();
            Closing?.Invoke(this, args);
            if (args.Cancel)
            {
                return;
            }
            // Otherwise, close the menu
            visible = false;
            Closed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adds a new item into the menu.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(SAItem item)
        {
            if (items.Contains(item))
            {
                throw new InvalidOperationException("Item is already part of the menu.");
            }
            items.Add(item);
            Recalculate();
        }
        /// <summary>
        /// Removes all of the items from the Menu.
        /// </summary>
        public void Clear()
        {
            items.Clear();
            Recalculate();
        }
        /// <summary>
        /// Checks if the item is present on the menu.
        /// </summary>
        /// <param name="item">THe item to check.</param>
        /// <returns></returns>
        public bool Contains(SAItem item) => items.Contains(item);
        /// <summary>
        /// Processes the menu contents.
        /// </summary>
        public void Process()
        {
            background.Draw();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw();
            }
        }
        /// <summary>
        /// Recalculates the position of the menu.
        /// </summary>
        public void Recalculate()
        {
            PointF pos = new PointF(69, 350);

            background.Position = pos;
            background.Size = new SizeF(497, (39 * items.Count) + 163);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].title.Position = new PointF(pos.X + 25, pos.Y + 96 + (39 * i));
            }
        }
        /// <summary>
        /// Removes a specific item from the menu.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(SAItem item)
        {
            if (items.Remove(item))
            {
                Recalculate();
            }
        }
        /// <summary>
        /// Removes the items that match the predicate.
        /// </summary>
        /// <param name="func">The predicate to match items.</param>
        public void Remove(Func<SAItem, bool> func)
        {
            if (items.RemoveAll(new Predicate<SAItem>(func)) > 0)
            {
                Recalculate();
            }
        }

        #endregion
    }
}
