namespace UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.tabObjects = new System.Windows.Forms.TabPage();
            this.tabExchanges = new System.Windows.Forms.TabPage();

            // USERS
            this.gridUsers = new System.Windows.Forms.DataGridView();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtPseudo = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();

            // OBJECTS
            this.gridObjects = new System.Windows.Forms.DataGridView();
            this.txtObjNom = new System.Windows.Forms.TextBox();
            this.comboOwner = new System.Windows.Forms.ComboBox();
            this.comboObjType = new System.Windows.Forms.ComboBox();
            this.comboObjEtat = new System.Windows.Forms.ComboBox();
            this.chkDisponible = new System.Windows.Forms.CheckBox();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.txtSearchPseudo = new System.Windows.Forms.TextBox();
            this.comboTypeFilter = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();

            // EXCHANGES
            this.gridExchanges = new System.Windows.Forms.DataGridView();
            this.comboFromUser = new System.Windows.Forms.ComboBox();
            this.comboToUser = new System.Windows.Forms.ComboBox();
            this.comboObjetPropose = new System.Windows.Forms.ComboBox();
            this.comboObjetDemande = new System.Windows.Forms.ComboBox();
            this.btnProposeExchange = new System.Windows.Forms.Button();
            this.btnAcceptExchange = new System.Windows.Forms.Button();
            this.btnRefuseExchange = new System.Windows.Forms.Button();
            this.btnExportJson = new System.Windows.Forms.Button();

            // FORM
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Text = "Plateforme d’échange d’objets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // TAB CONTROL
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabObjects);
            this.tabControl1.Controls.Add(this.tabExchanges);

            // TAB USERS
            this.tabUsers.Text = "Utilisateurs";
            this.tabUsers.Controls.Add(this.gridUsers);
            this.tabUsers.Controls.Add(this.txtNom);
            this.tabUsers.Controls.Add(this.txtPrenom);
            this.tabUsers.Controls.Add(this.txtPseudo);
            this.tabUsers.Controls.Add(this.txtEmail);
            this.tabUsers.Controls.Add(this.txtPassword);
            this.tabUsers.Controls.Add(this.btnAddUser);
            this.tabUsers.Controls.Add(this.btnDeleteUser);

            this.gridUsers.Location = new System.Drawing.Point(10, 10);
            this.gridUsers.Size = new System.Drawing.Size(700, 600);

            this.txtNom.Location = new System.Drawing.Point(750, 30);
            this.txtNom.PlaceholderText = "Nom";

            this.txtPrenom.Location = new System.Drawing.Point(750, 70);
            this.txtPrenom.PlaceholderText = "Prénom";

            this.txtPseudo.Location = new System.Drawing.Point(750, 110);
            this.txtPseudo.PlaceholderText = "Pseudo";

            this.txtEmail.Location = new System.Drawing.Point(750, 150);
            this.txtEmail.PlaceholderText = "Email";

            this.txtPassword.Location = new System.Drawing.Point(750, 190);
            this.txtPassword.PlaceholderText = "Mot de passe";
            this.txtPassword.PasswordChar = '*';

            this.btnAddUser.Location = new System.Drawing.Point(750, 240);
            this.btnAddUser.Text = "Ajouter utilisateur";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);

            this.btnDeleteUser.Location = new System.Drawing.Point(750, 280);
            this.btnDeleteUser.Text = "Supprimer utilisateur";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            // TAB OBJECTS
            this.tabObjects.Text = "Objets";
            this.tabObjects.Controls.Add(this.gridObjects);
            this.tabObjects.Controls.Add(this.txtObjNom);
            this.tabObjects.Controls.Add(this.comboOwner);
            this.tabObjects.Controls.Add(this.comboObjType);
            this.tabObjects.Controls.Add(this.comboObjEtat);
            this.tabObjects.Controls.Add(this.chkDisponible);
            this.tabObjects.Controls.Add(this.btnAddObject);
            this.tabObjects.Controls.Add(this.btnDeleteObject);
            this.tabObjects.Controls.Add(this.txtSearchPseudo);
            this.tabObjects.Controls.Add(this.comboTypeFilter);
            this.tabObjects.Controls.Add(this.btnSearch);

            this.gridObjects.Location = new System.Drawing.Point(10, 10);
            this.gridObjects.Size = new System.Drawing.Size(700, 600);

            this.txtObjNom.Location = new System.Drawing.Point(750, 30);
            this.txtObjNom.PlaceholderText = "Nom de l'objet";

            this.comboOwner.Location = new System.Drawing.Point(750, 70);
            this.comboOwner.Width = 200;

            this.comboObjType.Location = new System.Drawing.Point(750, 110);
            this.comboObjType.Width = 200;

            this.comboObjEtat.Location = new System.Drawing.Point(750, 150);
            this.comboObjEtat.Width = 200;

            this.chkDisponible.Location = new System.Drawing.Point(750, 190);
            this.chkDisponible.Text = "Disponible";

            this.btnAddObject.Location = new System.Drawing.Point(750, 230);
            this.btnAddObject.Text = "Ajouter objet";
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);

            this.btnDeleteObject.Location = new System.Drawing.Point(750, 270);
            this.btnDeleteObject.Text = "Supprimer objet";
            this.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteObject_Click);

            this.txtSearchPseudo.Location = new System.Drawing.Point(750, 330);
            this.txtSearchPseudo.PlaceholderText = "Recherche pseudo";

            this.comboTypeFilter.Location = new System.Drawing.Point(750, 370);
            this.comboTypeFilter.Width = 200;

            this.btnSearch.Location = new System.Drawing.Point(750, 410);
            this.btnSearch.Text = "Rechercher";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // TAB EXCHANGES
            this.tabExchanges.Text = "Échanges";
            this.tabExchanges.Controls.Add(this.gridExchanges);
            this.tabExchanges.Controls.Add(this.comboFromUser);
            this.tabExchanges.Controls.Add(this.comboToUser);
            this.tabExchanges.Controls.Add(this.comboObjetPropose);
            this.tabExchanges.Controls.Add(this.comboObjetDemande);
            this.tabExchanges.Controls.Add(this.btnProposeExchange);
            this.tabExchanges.Controls.Add(this.btnAcceptExchange);
            this.tabExchanges.Controls.Add(this.btnRefuseExchange);
            this.tabExchanges.Controls.Add(this.btnExportJson);

            this.gridExchanges.Location = new System.Drawing.Point(10, 10);
            this.gridExchanges.Size = new System.Drawing.Size(700, 600);

            this.comboFromUser.Location = new System.Drawing.Point(750, 30);
            this.comboFromUser.Width = 200;

            this.comboToUser.Location = new System.Drawing.Point(750, 70);
            this.comboToUser.Width = 200;

            this.comboObjetPropose.Location = new System.Drawing.Point(750, 110);
            this.comboObjetPropose.Width = 200;

            this.comboObjetDemande.Location = new System.Drawing.Point(750, 150);
            this.comboObjetDemande.Width = 200;

            this.btnProposeExchange.Location = new System.Drawing.Point(750, 190);
            this.btnProposeExchange.Text = "Proposer échange";
            this.btnProposeExchange.Click += new System.EventHandler(this.btnProposeExchange_Click);

            this.btnAcceptExchange.Location = new System.Drawing.Point(750, 230);
            this.btnAcceptExchange.Text = "Accepter";
            this.btnAcceptExchange.Click += new System.EventHandler(this.btnAcceptExchange_Click);

            this.btnRefuseExchange.Location = new System.Drawing.Point(750, 270);
            this.btnRefuseExchange.Text = "Refuser";
            this.btnRefuseExchange.Click += new System.EventHandler(this.btnRefuseExchange_Click);

            this.btnExportJson.Location = new System.Drawing.Point(750, 320);
            this.btnExportJson.Text = "Exporter JSON";
            this.btnExportJson.Click += new System.EventHandler(this.btnExportJson_Click);

            // ADD
            this.Controls.Add(this.tabControl1);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabObjects;
        private System.Windows.Forms.TabPage tabExchanges;

        private System.Windows.Forms.DataGridView gridUsers;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtPseudo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;

        private System.Windows.Forms.DataGridView gridObjects;
        private System.Windows.Forms.TextBox txtObjNom;
        private System.Windows.Forms.ComboBox comboOwner;
        private System.Windows.Forms.ComboBox comboObjType;
        private System.Windows.Forms.ComboBox comboObjEtat;
        private System.Windows.Forms.CheckBox chkDisponible;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.TextBox txtSearchPseudo;
        private System.Windows.Forms.ComboBox comboTypeFilter;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.DataGridView gridExchanges;
        private System.Windows.Forms.ComboBox comboFromUser;
        private System.Windows.Forms.ComboBox comboToUser;
        private System.Windows.Forms.ComboBox comboObjetPropose;
        private System.Windows.Forms.ComboBox comboObjetDemande;
        private System.Windows.Forms.Button btnProposeExchange;
        private System.Windows.Forms.Button btnAcceptExchange;
        private System.Windows.Forms.Button btnRefuseExchange;
        private System.Windows.Forms.Button btnExportJson;
    }
}
