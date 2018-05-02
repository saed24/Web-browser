using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/* *
 Main Window 
 * */

namespace WindowsFormsApplication1
{
    public partial class BrowserWindow : Form
    {
        private Point lastClickPos;
        private ContextMenuStrip contextMenuStrip;
       
       
        private TabControl tabControl;
        private TabPage tabPage;
        private List<RichTextBox> richTextBoxes;             

        private List<Stack<string>> backStacks, forwardStacks;
        private List<string> currentLinks;
        private int tabNumber;
        private string externalLink;

        private HomePage homePage;
        private string link;
        private WriteHistory writeHistory;

        private SaveFavourite saveFavourite;
        private ListHistory listHistory;
        private ListFavourites listFavourites;


        public BrowserWindow()
        {
            InitializeComponent();

            // Holds current links for each tab
            currentLinks = new List<string>();
            
            // Stacks to keep track of back and forward links for each tab
            backStacks = new List<Stack<string>>();
            forwardStacks = new List<Stack<string>>();

            // Rich Text Boxes for each tab in order to display HTML code
            richTextBoxes = new List<RichTextBox>();

            tabNumber = 0;
            

            // Create HomePage Object for loading the homepage into the first tab
            homePage = new HomePage();
            
            // Read stored homepage text
            link = homePage.read();

           
            // Put link in textbox
            urlTextbox.Text = link;


            // homepage is the current link for the first tab
            currentLinks.Add(link);
            

            // Initiate tabControl and tabPage
            startTabControl(); 
            
            // Initiate thread for loading homepage
            Shown += Form1_Shown;
            
            // Event handler for right mouse click
            this.tabControl.MouseClick += new MouseEventHandler(this.tabControl1_MouseClick);

            
            newTabButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;

            // Event handler for changing tabs
            this.tabControl.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);

            // Event handler for changing the size of the window
            this.SizeChanged += new EventHandler(this.browserWindow_SizeChanged);

            // Event handler for 'Return' key
            urlTextbox.KeyDown += new KeyEventHandler(urlTextBox_KeyDown);
            
            // Assign context menu strip to _CMS
            contextMenuStrip = GetCMS();

            // Disable back and forward buttons at startup 
            backButton.Enabled = false;           
            forwardButton.Enabled = false;
        }

        // Check for internet connection
        private bool hasInternetConnection()
        {
            try
            {
                IPHostEntry i = Dns.GetHostEntry("www.google.com");
                return true;
            }

            catch
            {
                return false;
            }
        }

        // Create thread on startup for loading homepage
        private void Form1_Shown(object sender, EventArgs e)
        {
            // Check if connected to internet
            if (hasInternetConnection() == true)
            {
                
                    // Thread Initialization with creating a Url object for retrieving HTML code
                    Thread t = new Thread(new ThreadStart(createURLThread));

                    t.SetApartmentState(ApartmentState.STA);
                    t.IsBackground = true;

                    // Start thread
                    t.Start();
                
            }

        }

        // On pressing 'Go' button
        private void Go_Click(object sender, EventArgs e)
        {
            // Check if connected to internet
            if (hasInternetConnection() == true)
            {
                if (urlTextbox.Text.Length > 0)
                {
                    goNextPageActions();

                    // Thread Initialization with creating a Url object for retrieving HTML code
                    Thread t = new Thread(new ThreadStart(createURLThread));
                    t.SetApartmentState(ApartmentState.STA);
                    t.IsBackground = true;

                    // Start thread
                    t.Start();
                }
            }
        }

        // On pressing 'Return' key
        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Check if connected to internet
                if (hasInternetConnection() == true)
                {
                    if (urlTextbox.Text.Length > 0)
                    {
                        goNextPageActions();

                        // Thread Initialization with creating a Url object for retrieving HTML code
                        Thread t = new Thread(new ThreadStart(createURLThread));
                        t.SetApartmentState(ApartmentState.STA);
                        t.IsBackground = true;

                        // Start thread
                        t.Start();
                    }
                }
            }
        }


        private void goNextPageActions()
        {
            writeHistory = new WriteHistory();

            // Write link to text file for history
            writeHistory.writeHistory(urlTextbox.Text);

            // For keeping record of which tab the link was requested from
            tabNumber = tabControl.SelectedIndex;

            // Add previous link to stack for Navigation at a tab
            backStacks.ElementAt(tabControl.SelectedIndex).Push(currentLinks.ElementAt(tabControl.SelectedIndex));

            // Tab title
            tabControl.SelectedTab.Text = urlTextbox.Text;

            // Remove previous link at specific tab
            currentLinks.RemoveAt(tabControl.SelectedIndex);

            // Insert current link for a specific tab
            currentLinks.Insert(tabControl.SelectedIndex, urlTextbox.Text);

            // Enable button
            backButton.Enabled = true;

            // Check if forward stack has any links to navigate for a specific tab
            if (forwardStacks[tabControl.SelectedIndex].Count > 0)
            {
                // Clear forward stack at specific tab
                forwardStacks[tabControl.SelectedIndex].Clear();
                forwardButton.Enabled = false;
            }
        }
        
        // Method For retrieving HTML code 
        private void createURLThread()
        {
            Url urlRequest = new Url();   
    
            // Request for HTML code and display in rich text box 
            setPage(urlRequest.HttpGet(urlTextbox.Text));
        }

        // Method for returning HTML code 
        // For History or Favourites
        private void createURLThreadFromExternalForm()
        {
            Url urlRequest = new Url();
            setPageFromExternalForm(urlRequest.HttpGet(externalLink));
        }

        // Method for displaying HTML code 
        // Displays HTML code at tab requested from
        private void setPage(string text)
        {
            richTextBoxes[tabNumber].Text = text;     
        }


        // Method for displaying HTML code
        // Displays HTML code at selected tab
        // Used for history and favourites
        private void setPageFromExternalForm(string text)
        {
            richTextBoxes[tabControl.SelectedIndex].Text = text;
        }

        // Method for history and favourites
        public void startUrlActions(string text)
        {
            // Check for internet connection
            if (hasInternetConnection() == true)
            {
                // Link from history or favourites assigned to glabal variable externalLink to request HTML code
                externalLink = text;


                urlTextbox.Text = externalLink;
                goNextPageActions();

                // Thread Initialization with creating a Url object for retrieving HTML code
                Thread t = new Thread(new ThreadStart(createURLThreadFromExternalForm));
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();

            }
        }

        // Method click on homepage
        // Makes current link the homepage
        private void homepage_Click(object sender, EventArgs e)
        {
            homePage = new HomePage();

            if(urlTextbox.Text.Length > 0)
            {
                homePage.write(urlTextbox.Text);
            }
            
        }

        // Open history window
        private void history_Click(object sender, EventArgs e)
        {
            listHistory = new ListHistory(this);
            listHistory.Show();
        }


        // Open window for saving favourite
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFavourite = new SaveFavourite(this);
            saveFavourite.Show();

            // Pass current link for saving as favourite 
            saveFavourite.setLabel(urlTextbox.Text);
        }

        // Open for listing favourites
        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listFavourites = new ListFavourites(this);
            listFavourites.Show();
        }

        // Method for retrieving HTML code
        // Link is retrieved from urlTextbox which was assigned from stack
        private void callStacksLink()
        {
            Url urlRequest = new Url();
            setPage(urlRequest.HttpGet(urlTextbox.Text));
        }

        // Method for 'Back' button
        private void back_Click(object sender, EventArgs e)
        {
            // Check for internet connection
            if (hasInternetConnection() == true)
            {
                // Assign current tab page index to global variable tab number
                tabNumber = tabControl.SelectedIndex;

                // Push current link to forward stack at specified tab index
                forwardStacks.ElementAt(tabControl.SelectedIndex).Push(currentLinks.ElementAt(tabControl.SelectedIndex));

                // Remove current link 
                currentLinks.RemoveAt(tabControl.SelectedIndex);

                // Make previous link to current link and pop previous link from stack at specified tab index
                currentLinks.Insert(tabControl.SelectedIndex, backStacks.ElementAt(tabControl.SelectedIndex).Pop());

                // Copy current link to urlTextbox
                urlTextbox.Text = currentLinks.ElementAt(tabControl.SelectedIndex);

                // Make current tab title to current link 
                tabControl.SelectedTab.Text = urlTextbox.Text;

                // For new tabs because there are no previous links
                if (currentLinks.ElementAt(tabControl.SelectedIndex).Length == 0)
                {
                    //  set page to empty because of no previous links for new tabs
                    setPage("");
                }

                else
                {
                    // Thread Initialization with creating a Url object for retrieving HTML code
                    Thread t = new Thread(new ThreadStart(callStacksLink));
                    t.SetApartmentState(ApartmentState.STA);
                    t.IsBackground = true;
                    t.Start();
                }
            }

            // If there is no previous link at specified tab, then disable backButton
            if (backStacks[tabControl.SelectedIndex].Count == 0)
            {
                backButton.Enabled = false;
            }

            else
            {
                backButton.Enabled = true;
            }

            // If there is no previous link at specified tab, then disable forwardButton
            if (forwardStacks[tabControl.SelectedIndex].Count == 0)
            {
                forwardButton.Enabled = false;
            }

            else
            {
                forwardButton.Enabled = true;
            } 
        }

        // Method for 'Forward' button
        private void forward_Click(object sender, EventArgs e)
        {
            // Check for internet connection
            if (hasInternetConnection() == true)
            {
                // Assign current tab page index to global variable tab number
                tabNumber = tabControl.SelectedIndex;

                // Push current link to backStack at specific tab
                backStacks[tabControl.SelectedIndex].Push(currentLinks[tabControl.SelectedIndex]);

                // Remove current link from list at specified tab
                currentLinks.RemoveAt(tabControl.SelectedIndex);

                // Get next link from forward stack and assign it to current link list at specific tab
                currentLinks.Insert(tabControl.SelectedIndex, forwardStacks[tabControl.SelectedIndex].Pop());

                // Assign urlTextbox to current link at specified tab
                urlTextbox.Text = currentLinks.ElementAt(tabControl.SelectedIndex);

                // Make current tab title to current link 
                tabControl.SelectedTab.Text = urlTextbox.Text;

                // If there is no previous link at specified tab, then disable backButton
                if (backStacks[tabControl.SelectedIndex].Count == 0)
                {
                    backButton.Enabled = false;
                }

                else
                {
                    backButton.Enabled = true;
                }

                // If there is no next link at specified tab, then disable forwardButton
                if (forwardStacks[tabControl.SelectedIndex].Count == 0)
                {
                    forwardButton.Enabled = false;
                }

                else
                {
                    forwardButton.Enabled = true;
                }

            }
            Thread t = new Thread(new ThreadStart(callStacksLink));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();

                        
        }

        // Method for displaying a shorcut menu
        private ContextMenuStrip GetCMS()
        {
            ContextMenuStrip CMS = new ContextMenuStrip();


            CMS.Items.Add("Close", null, new EventHandler(Item_Clicked));
            return CMS;
        }

        private void Item_Clicked(object sender, EventArgs E)
        {
            for (int i = 0; i < tabControl.TabCount; i++)
            {
                // Create a rectangle 
                Rectangle rect = tabControl.GetTabRect(i);

                // if the position clicked is on a tab
                if (rect.Contains(tabControl.PointToClient(lastClickPos)))
                {
                    // remove the richTextBox
                    richTextBoxes.RemoveAt(tabControl.SelectedIndex);

                    // remove the stacks at specified tab
                    backStacks.RemoveAt(tabControl.SelectedIndex);
                    forwardStacks.RemoveAt(tabControl.SelectedIndex);

                    // remove current link at specified tab
                    currentLinks.RemoveAt(tabControl.SelectedIndex);

                    // remove the tab page 
                    tabControl.TabPages.RemoveAt(i);
                }
            }
        }

        // Method for right clicking
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lastClickPos = Cursor.Position;
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        // Method for tab change
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if current link at specified tab is in the list         
            if(currentLinks.ElementAt(tabControl.SelectedIndex).Length > 0)
            {
                // Assign urlTextbox to current link at specified tab when tab index changes
                urlTextbox.Text = currentLinks.ElementAt(tabControl.SelectedIndex);
            }

            // if not in the list
            else
            {
                // Display empty string
                urlTextbox.Text = "";
            }

            // If there is no next link at specified tab, then disable forwardButton
            if (forwardStacks[tabControl.SelectedIndex].Count == 0)
            {
                forwardButton.Enabled = false;
            }

            else
            {
                forwardButton.Enabled = true;
            }

            // If there is no previous link at specified tab, then disable backButton
            if (backStacks[tabControl.SelectedIndex].Count == 0)
            {
                backButton.Enabled = false;
            }

            else
            {
                backButton.Enabled = true;
            }
        }

       
        // Method for adding tab
        private void addTab_Click(object sender, EventArgs e)
        {
            // Create new tab page
            TabPage tb = new TabPage();

            // Make the size of tab page according to window
            tb.Size = new System.Drawing.Size(this.Size.Width - 25, this.Size.Height - 110);

            // Add tab page to tab control
            tabControl.Controls.Add(tb);
            
            // Make new richTextBox
            RichTextBox textBox = new RichTextBox();
            textBox.Multiline = true;
            textBox.WordWrap = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;

            // Make size of textbox according to window
            textBox.Size = new System.Drawing.Size(this.Size.Width - 30, this.Size.Height - 110);
            
            // add textbox to the list of richTextBoxes
            richTextBoxes.Add(textBox);

            // Add textBox to new tab
            tb.Controls.Add(textBox);

            // Create backStack and forwardStack
            Stack<string> backStack = new Stack<string>();
            Stack<string> forwardStack = new Stack<string>();

            // add backStack to the list of backStacks for new tab
            backStacks.Add(backStack);

            // add forwardStack to the list of forwardStacks for new tab
            forwardStacks.Add(forwardStack);

            // add empty string for new tab
            currentLinks.Add("");    
        }

        
        // Create Tab control and first tab page
        private void startTabControl()
        {
            
            this.tabControl = new System.Windows.Forms.TabControl();
 
            this.tabControl.Location = new System.Drawing.Point(0, 59);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(this.Size.Width - 25, this.Size.Height - 110);
            this.tabControl.TabIndex = 7;

            
            tabPage = new System.Windows.Forms.TabPage();
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabPage";
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(this.Size.Width - 25, this.Size.Height - 110);
            tabPage.TabIndex = 0;
            tabPage.Text = urlTextbox.Text;
            tabPage.UseVisualStyleBackColor = true;
            
            // make new textbox
            RichTextBox textBox = new RichTextBox();
            textBox.Multiline = true;
            textBox.WordWrap = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            textBox.Size = new System.Drawing.Size(this.Size.Width - 30, this.Size.Height - 110);

            // add textbox to the list of richTextBoxes
            richTextBoxes.Add(textBox);

            // Add textBox to new tab
            tabPage.Controls.Add(textBox);

            // Create backStack and forwardStack
            tabControl.Controls.Add(tabPage);

            // Add tab page to tab control
            Controls.Add(this.tabControl);

            // Create backStack and forwardStack
            Stack<string> backStack = new Stack<string>();   
            Stack<string> forwardStack = new Stack<string>();

            // add backStack to the list of backStacks for new tab
            backStacks.Add(backStack);

            // add forwarStack to the list of forwardStacks for new tab
            forwardStacks.Add(forwardStack);

        }

        // Method for resizing tabControl, tabPage and richTextBoxes according to Main window
        private void browserWindow_SizeChanged(object sender, System.EventArgs e)
        {
            this.tabControl.Size = new System.Drawing.Size(this.Size.Width - 25,this.Size.Height -110 );//406
            tabPage.Size = new System.Drawing.Size(this.Size.Width - 25, this.Size.Height -110 );//380
           
            for(int idx = 0; idx < tabControl.TabPages.Count; idx++)
            {
                richTextBoxes[idx].Size = new System.Drawing.Size(this.Size.Width - 30, this.Size.Height - 110);
            }

            TabControl.TabPageCollection pages = tabControl.TabPages;
            
            foreach (TabPage page in pages)
            {
                page.Size = new System.Drawing.Size(this.Size.Width - 25, this.Size.Height - 110);       
            }
        }
    }
}
