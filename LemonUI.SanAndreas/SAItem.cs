#if FIVEM
using Font = CitizenFX.Core.UI.Font;
using Alignment = CitizenFX.Core.UI.Alignment;
#elif SHVDN3
using Font = GTA.UI.Font;
using Alignment = GTA.UI.Alignment;
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

        private static readonly Color colorSelected = Color.FromArgb(255, 168, 199, 237);
        private static readonly Color colorNotSelected = Color.FromArgb(255, 70, 86, 104);
        private static readonly Color colorDisabled = Color.FromArgb(255, 146, 146, 146);
        private static readonly Color colorHeader = Color.FromArgb(255, 221, 221, 221);

        private PointF lastPos = PointF.Empty;
        private float lastWidth = 0;
        private bool lastSelected = false;
        private bool lastHeader = false;

        private bool enabled = true;
        internal readonly ScaledText title = new ScaledText(PointF.Empty, "", 0.68f, (Font)3)
        {
            Shadow = true
        };
        internal readonly ScaledText subtitle = new ScaledText(PointF.Empty, "", 0.68f, (Font)3)
        {
            Alignment = Alignment.Right,
            Shadow = true
        };

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
                UpdateColor(value, lastHeader);
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
        /// <summary>
        /// Creates a new item with a title and subtitle.
        /// </summary>
        public SAItem(string title, string subtitle)
        {
            this.title.Text = title;
            this.subtitle.Text = subtitle;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the colors of the text.
        /// </summary>
        /// <param name="selected">If the item is selected or not.</param>
        /// <param name="header">If the item is a menu header or not.</param>
        private void UpdateColor(bool selected, bool header)
        {
            if (header)
            {
                title.Color = colorHeader;
                subtitle.Color = colorHeader;
            }
            else if (!enabled)
            {
                title.Color = colorDisabled;
                subtitle.Color = colorDisabled;
            }
            else if (selected)
            {
                title.Color = colorSelected;
                subtitle.Color = colorSelected;
            }
            else
            {
                title.Color = colorNotSelected;
                subtitle.Color = colorNotSelected;
            }
        }
        /// <summary>
        /// Recalculates the position of the item.
        /// </summary>
        /// <param name="pos">The base position of the item.</param>
        /// <param name="width">The width of the menu.</param>
        /// <param name="selected">If the item is selected or not.</param>
        public void Recalculate(PointF pos, float width, bool selected) => Recalculate(pos, width, selected, false);
        /// <summary>
        /// Recalculates the position of the item with the last known values.
        /// </summary>
        private void Recalculate() => Recalculate(lastPos, lastWidth, lastSelected);
        /// <summary>
        /// Recalculates the position of the item.
        /// </summary>
        /// <param name="pos">The base position of the item.</param>
        /// <param name="width">The width of the menu.</param>
        /// <param name="selected">If the item is selected or not.</param>
        /// <param name="header">If the item is a menu header.</param>
        internal void Recalculate(PointF pos, float width, bool selected, bool header)
        {
            lastPos = pos;
            lastWidth = width;
            lastSelected = selected;
            lastHeader = header;

            UpdateColor(selected, header);

            title.Position = new PointF(pos.X + 24, pos.Y);
            subtitle.Position = new PointF(pos.X + width - 24, pos.Y);
        }
        /// <summary>
        /// Draws the item.
        /// </summary>
        public void Draw()
        {
            title.Draw();
            subtitle.Draw();
        }

        #endregion
    }
}
