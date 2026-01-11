namespace UI
{
    partial class LoginWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;

        // Connexion
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.TextBox txtPseudo;
        private System.Windows.Forms.Button btnLogin;

        // Création de compte
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtPseudoNew;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnCreate;

        private System.Windows.Forms.Label lblError;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();

            this.lblConn = new System.Windows.Forms.Label();
            this.txtPseudo = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();

            this.lblCreate = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtPseudoNew = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();

            this.lblError = new System.Windows.Forms.Label();

            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(520, 430);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion";

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(190, 15);
            this.lblTitle.Text = "Plateforme";

            // --- Connexion ---
            this.lblConn.AutoSize = true;
            this.lblConn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConn.Location = new System.Drawing.Point(30, 60);
            this.lblConn.Text = "Se connecter";

            this.txtPseudo.Location = new System.Drawing.Point(30, 95);
            this.txtPseudo.Size = new System.Drawing.Size(450, 27);
            this.txtPseudo.PlaceholderText = "Pseudo";

            this.btnLogin.Location = new System.Drawing.Point(30, 130);
            this.btnLogin.Size = new System.Drawing.Size(450, 35);
            this.btnLogin.Text = "Connexion";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // --- Création compte ---
            this.lblCreate.AutoSize = true;
            this.lblCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreate.Location = new System.Drawing.Point(30, 190);
            this.lblCreate.Text = "Créer un compte";

            this.txtNom.Location = new System.Drawing.Point(30, 225);
            this.txtNom.Size = new System.Drawing.Size(220, 27);
            this.txtNom.PlaceholderText = "Nom";

            this.txtPrenom.Location = new System.Drawing.Point(260, 225);
            this.txtPrenom.Size = new System.Drawing.Size(220, 27);
            this.txtPrenom.PlaceholderText = "Prénom";

            this.txtPseudoNew.Location = new System.Drawing.Point(30, 265);
            this.txtPseudoNew.Size = new System.Drawing.Size(450, 27);
            this.txtPseudoNew.PlaceholderText = "Pseudo (unique)";

            this.txtEmail.Location = new System.Drawing.Point(30, 305);
            this.txtEmail.Size = new System.Drawing.Size(450, 27);
            this.txtEmail.PlaceholderText = "Email (optionnel)";

            this.btnCreate.Location = new System.Drawing.Point(30, 340);
            this.btnCreate.Size = new System.Drawing.Size(450, 35);
            this.btnCreate.Text = "Créer le compte";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);

            // --- Erreur ---
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblError.Location = new System.Drawing.Point(30, 385);
            this.lblError.Text = "";

            // Add controls
            this.Controls.Add(this.lblTitle);

            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.txtPseudo);
            this.Controls.Add(this.btnLogin);

            this.Controls.Add(this.lblCreate);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.txtPseudoNew);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnCreate);

            this.Controls.Add(this.lblError);

            this.AcceptButton = this.btnLogin;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
