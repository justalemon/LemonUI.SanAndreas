#if FIVEM
using Font = CitizenFX.Core.UI.Font;
#elif SHVDN3
using Font = GTA.UI.Font;
#endif
using LemonUI.Elements;
using LemonUI.Menus;
using System;
using System.Drawing;

namespace LemonUI.SanAndreas
{
    /// <summary>
    /// Simple item used on the San Andreas menus.
    /// </summary>
    public class SAItem
    {
        #region Fields

        private bool enabled = true;
        internal readonly ScaledText title = new ScaledText(PointF.Empty, "", 0.68f, (Font)3);

        #endregion

        #region Properties

        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value)
                {
                    return;
                }
                enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item is selected.
        /// </summary>
        public event SelectedEventHandler Selected;
        /// <summary>
        /// Event triggered when the item is activated.
        /// </summary>
        public event EventHandler Activated;
        /// <summary>
        /// Event triggered when the <see cref="Enabled"/> property is changed.
        /// </summary>
        public event EventHandler EnabledChanged;

        /// <summary>
        /// Triggers the Selected event.
        /// </summary>
        protected internal void OnSelected(object sender, SelectedEventArgs e) => Selected?.Invoke(sender, e);
        /// <summary>
        /// Triggers the Activated event.
        /// </summary>
        protected internal void OnActivated(object sender) => Activated?.Invoke(sender, EventArgs.Empty);

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new item with a title.
        /// </summary>
        public SAItem(string title)
        {
            this.title.Text = title;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Recalculates the position of the menu with the last known values.
        /// </summary>
        private void Recalculate()
        {

        }
        /// <summary>
        /// Draws the item.
        /// </summary>
        public void Draw()
        {
            title.Draw();
        }

        #endregion
    }
}
