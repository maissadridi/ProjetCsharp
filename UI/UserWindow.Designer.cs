namespace UI
{
    partial class UserWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProfil;
        private System.Windows.Forms.TabPage tabObjets;
        private System.Windows.Forms.TabPage tabEchanges;

        private System.Windows.Forms.Label lblPseudo;
        private System.Windows.Forms.Label lblNomPrenom;
        private System.Windows.Forms.Label lblPoints;

        private System.Windows.Forms.DataGridView gridMyObjects;
        private System.Windows.Forms.TextBox txtObjNom;
        private System.Windows.Forms.ComboBox comboObjType;
        private System.Windows.Forms.ComboBox comboObjEtat;
        private System.Windows.Forms.CheckBox chkDisponible;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.Button btnToggleDisponibilite;

        private System.Windows.Forms.DataGridView gridMyExchanges;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnRefuse;

        private System.Windows.Forms.Label lblProposeTitle;
        private System.Windows.Forms.ComboBox comboTargetUser;
        private System.Windows.Forms.ComboBox comboMyObjectPropose;
        private System.Windows.Forms.ComboBox comboTargetObjectDemande;
        private System.Windows.Forms.CheckBox chkDon;
        private System.Windows.Forms.Button btnProposeExchange;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProfil = new System.Windows.Forms.TabPage();
            this.tabObjets = new System.Windows.Forms.TabPage();
            this.tabEchanges = new System.Windows.Forms.TabPage();

            this.lblPseudo = new System.Windows.Forms.Label();
            this.lblNomPrenom = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();

            this.gridMyObjects = new System.Windows.Forms.DataGridView();
            this.txtObjNom = new System.Windows.Forms.TextBox();
            this.comboObjType = new System.Windows.Forms.ComboBox();
            this.comboObjEtat = new System.Windows.Forms.ComboBox();
            this.chkDisponible = new System.Windows.Forms.CheckBox();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.btnToggleDisponibilite = new System.Windows.Forms.Button();

            this.gridMyExchanges = new System.Windows.Forms.DataGridView();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRefuse = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mon espace";

            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(20, 15);
            this.lblHeader.Text = "Mon espace utilisateur";

            this.tabControl1.Location = new System.Drawing.Point(20, 55);
            this.tabControl1.Size = new System.Drawing.Size(1050, 600);
            this.tabControl1.Controls.Add(this.tabProfil);
            this.tabControl1.Controls.Add(this.tabObjets);
            this.tabControl1.Controls.Add(this.tabEchanges);

            // PROFIL
            this.tabProfil.Text = "Profil";
            this.tabProfil.Controls.Add(this.lblPseudo);
            this.tabProfil.Controls.Add(this.lblNomPrenom);
            this.tabProfil.Controls.Add(this.lblPoints);

            this.lblPseudo.AutoSize = true;
            this.lblPseudo.Location = new System.Drawing.Point(30, 40);

            this.lblNomPrenom.AutoSize = true;
            this.lblNomPrenom.Location = new System.Drawing.Point(30, 75);

            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPoints.Location = new System.Drawing.Point(30, 115);

            // OBJETS
            this.tabObjets.Text = "Mes objets";
            this.tabObjets.Controls.Add(this.gridMyObjects);
            this.tabObjets.Controls.Add(this.txtObjNom);
            this.tabObjets.Controls.Add(this.comboObjType);
            this.tabObjets.Controls.Add(this.comboObjEtat);
            this.tabObjets.Controls.Add(this.chkDisponible);
            this.tabObjets.Controls.Add(this.btnAddObject);
            this.tabObjets.Controls.Add(this.btnDeleteObject);
            this.tabObjets.Controls.Add(this.btnToggleDisponibilite);

            this.gridMyObjects.Location = new System.Drawing.Point(10, 10);
            this.gridMyObjects.Size = new System.Drawing.Size(700, 520);

            this.txtObjNom.Location = new System.Drawing.Point(740, 30);
            this.txtObjNom.Size = new System.Drawing.Size(250, 27);
            this.txtObjNom.PlaceholderText = "Nom de l'objet";

            this.comboObjType.Location = new System.Drawing.Point(740, 70);
            this.comboObjType.Size = new System.Drawing.Size(250, 28);

            this.comboObjEtat.Location = new System.Drawing.Point(740, 110);
            this.comboObjEtat.Size = new System.Drawing.Size(250, 28);

            this.chkDisponible.Location = new System.Drawing.Point(740, 150);
            this.chkDisponible.Text = "Disponible";

            this.btnAddObject.Location = new System.Drawing.Point(740, 190);
            this.btnAddObject.Size = new System.Drawing.Size(250, 35);
            this.btnAddObject.Text = "Ajouter";
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);

            this.btnDeleteObject.Location = new System.Drawing.Point(740, 235);
            this.btnDeleteObject.Size = new System.Drawing.Size(250, 35);
            this.btnDeleteObject.Text = "Supprimer sélection";
            this.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteObject_Click);

            this.btnToggleDisponibilite.Location = new System.Drawing.Point(740, 280);
            this.btnToggleDisponibilite.Size = new System.Drawing.Size(250, 35);
            this.btnToggleDisponibilite.Text = "Basculer disponibilité";
            this.btnToggleDisponibilite.Click += new System.EventHandler(this.btnToggleDisponibilite_Click);

            // ECHANGES
            this.tabEchanges.Text = "Mes échanges";
            this.tabEchanges.Controls.Add(this.gridMyExchanges);
            this.tabEchanges.Controls.Add(this.btnAccept);
            this.tabEchanges.Controls.Add(this.btnRefuse);

            this.lblProposeTitle = new System.Windows.Forms.Label();
            this.comboTargetUser = new System.Windows.Forms.ComboBox();
            this.comboMyObjectPropose = new System.Windows.Forms.ComboBox();
            this.comboTargetObjectDemande = new System.Windows.Forms.ComboBox();
            this.chkDon = new System.Windows.Forms.CheckBox();
            this.btnProposeExchange = new System.Windows.Forms.Button();

            this.tabEchanges.Controls.Add(this.lblProposeTitle);
            this.tabEchanges.Controls.Add(this.comboTargetUser);
            this.tabEchanges.Controls.Add(this.comboMyObjectPropose);
            this.tabEchanges.Controls.Add(this.comboTargetObjectDemande);
            this.tabEchanges.Controls.Add(this.chkDon);
            this.tabEchanges.Controls.Add(this.btnProposeExchange);


            // ---- Zone "Proposer un échange" ----
            this.lblProposeTitle.AutoSize = true;
            this.lblProposeTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProposeTitle.Location = new System.Drawing.Point(740, 150);
            this.lblProposeTitle.Text = "Proposer un échange";

            this.comboMyObjectPropose.Location = new System.Drawing.Point(740, 185);
            this.comboMyObjectPropose.Size = new System.Drawing.Size(250, 28);
            this.comboMyObjectPropose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.comboTargetUser.Location = new System.Drawing.Point(740, 225);
            this.comboTargetUser.Size = new System.Drawing.Size(250, 28);
            this.comboTargetUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.comboTargetObjectDemande.Location = new System.Drawing.Point(740, 265);
            this.comboTargetObjectDemande.Size = new System.Drawing.Size(250, 28);
            this.comboTargetObjectDemande.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.chkDon.Location = new System.Drawing.Point(740, 305);
            this.chkDon.Text = "Je donne (pas d'objet demandé)";
            this.chkDon.CheckedChanged += new System.EventHandler(this.chkDon_CheckedChanged);

            this.btnProposeExchange.Location = new System.Drawing.Point(740, 340);
            this.btnProposeExchange.Size = new System.Drawing.Size(250, 35);
            this.btnProposeExchange.Text = "Envoyer la proposition";
            this.btnProposeExchange.Click += new System.EventHandler(this.btnProposeExchange_Click);


            this.gridMyExchanges.Location = new System.Drawing.Point(10, 10);
            this.gridMyExchanges.Size = new System.Drawing.Size(700, 520);

            this.btnAccept.Location = new System.Drawing.Point(740, 40);
            this.btnAccept.Size = new System.Drawing.Size(250, 35);
            this.btnAccept.Text = "Accepter (si je suis receveur)";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);

            this.btnRefuse.Location = new System.Drawing.Point(740, 85);
            this.btnRefuse.Size = new System.Drawing.Size(250, 35);
            this.btnRefuse.Text = "Refuser (si je suis receveur)";
            this.btnRefuse.Click += new System.EventHandler(this.btnRefuse_Click);



            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.tabControl1);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
