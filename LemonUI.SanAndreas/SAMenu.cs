using System;
using System.Collections.Generic;

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
        private readonly List<SAItem> items = new List<SAItem>();

        #endregion

        #region Properties

        /// <summary>
        /// If this menu is visible or not.
        /// </summary>
        public bool Visible
        {
            get => visible;
            set => throw new NotImplementedException();
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

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new menu with no title.
        /// </summary>
        public SAMenu() => Recalculate();

        #endregion

        #region Functions

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
        }
        /// <summary>
        /// Recalculates the position of the menu.
        /// </summary>
        public void Recalculate()
        {
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
