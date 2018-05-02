using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    partial class BrowserWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserWindow));
            this.page = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.Go = new System.Windows.Forms.ToolStripButton();
            this.homepage = new System.Windows.Forms.ToolStripButton();
            this.history = new System.Windows.Forms.ToolStripButton();
            this.favourite = new System.Windows.Forms.ToolStripDropDownButton();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTabButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // page
            // 
            this.page.Location = new System.Drawing.Point(0, 69);
            this.page.Name = "page";
            this.page.ReadOnly = true;
            this.page.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.page.Size = new System.Drawing.Size(845, 380);
            this.page.TabIndex = 3;
            this.page.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.urlTextbox,
            this.Go,
            this.homepage,
            this.history,
            this.favourite});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(916, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backButton
            // 
            this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(23, 22);
            this.backButton.Text = "toolStripButton1";
            this.backButton.Click += new System.EventHandler(this.back_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(23, 22);
            this.forwardButton.Text = "toolStripButton2";
            this.forwardButton.Click += new System.EventHandler(this.forward_Click);
            // 
            // urlTextbox
            // 
            this.urlTextbox.Name = "urlTextbox";
            this.urlTextbox.Size = new System.Drawing.Size(200, 25);
            // 
            // Go
            // 
            this.Go.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Go.Image = ((System.Drawing.Image)(resources.GetObject("Go.Image")));
            this.Go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(23, 22);
            this.Go.Text = "Go";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // homepage
            // 
            this.homepage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homepage.Image = ((System.Drawing.Image)(resources.GetObject("homepage.Image")));
            this.homepage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homepage.Name = "homepage";
            this.homepage.Size = new System.Drawing.Size(23, 22);
            this.homepage.Text = "Homepage";
            this.homepage.Click += new System.EventHandler(this.homepage_Click);
            // 
            // history
            // 
            this.history.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.history.Image = ((System.Drawing.Image)(resources.GetObject("history.Image")));
            this.history.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(23, 22);
            this.history.Text = "History";
            this.history.Click += new System.EventHandler(this.history_Click);
            // 
            // favourite
            // 
            this.favourite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.favourite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.listToolStripMenuItem});
            this.favourite.Image = ((System.Drawing.Image)(resources.GetObject("favourite.Image")));
            this.favourite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.favourite.Name = "favourite";
            this.favourite.Size = new System.Drawing.Size(29, 22);
            this.favourite.Text = "Favourite";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.createToolStripMenuItem.Text = "Save";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.listToolStripMenuItem.Text = "List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // newTabButton
            // 
            this.newTabButton.Location = new System.Drawing.Point(728, 28);
            this.newTabButton.Name = "newTabButton";
            this.newTabButton.Size = new System.Drawing.Size(117, 23);
            this.newTabButton.TabIndex = 6;
            this.newTabButton.Text = "Open New Tab";
            this.newTabButton.UseVisualStyleBackColor = true;
            this.newTabButton.Click += new System.EventHandler(this.addTab_Click);
            // 
            // BrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(916, 514);
            this.Controls.Add(this.newTabButton);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BrowserWindow";
            this.Text = "Browser Window";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox page;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripTextBox urlTextbox;
        private System.Windows.Forms.ToolStripButton Go;
        private System.Windows.Forms.ToolStripButton homepage;
        private System.Windows.Forms.ToolStripButton history;
        private System.Windows.Forms.ToolStripDropDownButton favourite;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private Button newTabButton;
    }
}

