using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.SanAndreas
{
    /// <summary>
    /// Simple item used on the San Andreas menus.
    /// </summary>
    public class SAItem
    {
        #region Fields

        internal readonly ScaledText title = new ScaledText(PointF.Empty, "");

        #endregion

        #region Properties

        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

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

        #endregion
    }
}
