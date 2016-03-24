using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// Stores the game hander.
        /// </summary>
        private StackerGame _GameHandler;

        /// <summary>
        /// Initialises this form.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The form load event handler.
        /// </summary>
        /// <param name="Sender">The object that fired this event.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void Main_Load(object Sender, EventArgs EventArgs)
        {
            // Create the stacker handler.
            this._GameHandler = new StackerGame(this);
        }

        /// <summary>
        /// The start/place button press event handler.
        /// </summary>
        /// <param name="Sender">The object that fired this event.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void StartButton_Click(object Sender, EventArgs EventArgs)
        {
            // Check the text.
            if (this.StartButton.Text == "START")
            {
                // Start a new game.
                this.StartButton.Text = "PLACE";
                this._GameHandler.GameInProgress = true;
            }
            else
            {
                // Place a block!
                this._GameHandler.PlaceBlock = true;
            }
        }
    }
}
