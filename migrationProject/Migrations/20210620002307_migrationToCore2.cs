using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace migrationProject.Migrations
{
    public partial class migrationToCore2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "corp",
                columns: table => new
                {
                    Idcorps = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corp", x => x.Idcorps);
                });

            migrationBuilder.CreateTable(
                name: "ordrevirement",
                columns: table => new
                {
                    IdOrdreVirement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idAdmin = table.Column<int>(type: "int", nullable: false),
                    numeroCompte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOrdrePayment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordrevirement", x => x.IdOrdreVirement);
                });

            migrationBuilder.CreateTable(
                name: "param_paiement",
                columns: table => new
                {
                    id_param_paiement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Art = table.Column<int>(type: "int", nullable: false),
                    num1 = table.Column<int>(type: "int", nullable: false),
                    num2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_param_paiement", x => x.id_param_paiement);
                });

            migrationBuilder.CreateTable(
                name: "param_voiture",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_cheveau_min = table.Column<int>(type: "int", nullable: false),
                    nombre_cheveau_max = table.Column<int>(type: "int", nullable: false),
                    prix_par_km = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_param_voiture", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parametre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valeur1 = table.Column<int>(type: "int", nullable: false),
                    valeur2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parametre_img",
                columns: table => new
                {
                    id_param_img = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    footer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    background = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametre_img", x => x.id_param_img);
                });

            migrationBuilder.CreateTable(
                name: "trajet",
                columns: table => new
                {
                    id_trajet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    villeDepart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    villeArrivee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    distance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trajet", x => x.id_trajet);
                });

            migrationBuilder.CreateTable(
                name: "cadres",
                columns: table => new
                {
                    IdCadre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCorps = table.Column<int>(type: "int", nullable: false),
                    CorpsIdcorps = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadres", x => x.IdCadre);
                    table.ForeignKey(
                        name: "FK_cadres_corp_CorpsIdcorps",
                        column: x => x.CorpsIdcorps,
                        principalTable: "corp",
                        principalColumn: "Idcorps",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grade",
                columns: table => new
                {
                    Idgrades = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nomarabe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCadre = table.Column<int>(type: "int", nullable: false),
                    CadreIdCadre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade", x => x.Idgrades);
                    table.ForeignKey(
                        name: "FK_grade_cadres_CadreIdCadre",
                        column: x => x.CadreIdCadre,
                        principalTable: "cadres",
                        principalColumn: "IdCadre",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personnels",
                columns: table => new
                {
                    IdPers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nomarabe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prenomarabe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DRPP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gsm1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gsm2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    liencv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceAffecte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SituationAdministratif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituationFamiliale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreEnfant = table.Column<int>(type: "int", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRecrutement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRecrutementENSA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEffetgrade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEffetEchelon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    renouvelement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diplome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ecolediplome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    anneediplome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    show1 = table.Column<bool>(type: "bit", nullable: false),
                    show2 = table.Column<bool>(type: "bit", nullable: false),
                    echelon = table.Column<int>(type: "int", nullable: false),
                    Echelle = table.Column<int>(type: "int", nullable: false),
                    TypeqEntiterecherches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nomentiterecherches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lieuentiterecherches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thematiquerecherches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Journauxrecherches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomCorps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomCadre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomGrades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idcorps = table.Column<int>(type: "int", nullable: false),
                    IdCadre = table.Column<int>(type: "int", nullable: false),
                    Idgrades = table.Column<int>(type: "int", nullable: false),
                    Typecontract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumContract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Société = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateContract = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CadreIdCadre = table.Column<int>(type: "int", nullable: true),
                    CorpsIdcorps = table.Column<int>(type: "int", nullable: true),
                    GradesIdgrades = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personnels", x => x.IdPers);
                    table.ForeignKey(
                        name: "FK_personnels_cadres_CadreIdCadre",
                        column: x => x.CadreIdCadre,
                        principalTable: "cadres",
                        principalColumn: "IdCadre",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personnels_corp_CorpsIdcorps",
                        column: x => x.CorpsIdcorps,
                        principalTable: "corp",
                        principalColumn: "Idcorps",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personnels_grade_GradesIdgrades",
                        column: x => x.GradesIdgrades,
                        principalTable: "grade",
                        principalColumn: "Idgrades",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ordremission",
                columns: table => new
                {
                    idOrdremission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    etat = table.Column<bool>(type: "bit", nullable: false),
                    name_respo_mission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateDepart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateArrivee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    heureDepart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    heureArrivee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    echlon = table.Column<int>(type: "int", nullable: false),
                    matricule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    objetDepart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    moyenTransport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreCheuvaux = table.Column<int>(type: "int", nullable: false),
                    montant_total = table.Column<float>(type: "real", nullable: false),
                    IdPers = table.Column<int>(type: "int", nullable: false),
                    id_trajet = table.Column<int>(type: "int", nullable: false),
                    PersonnelIdPers = table.Column<int>(type: "int", nullable: true),
                    Trajetid_trajet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordremission", x => x.idOrdremission);
                    table.ForeignKey(
                        name: "FK_ordremission_personnels_PersonnelIdPers",
                        column: x => x.PersonnelIdPers,
                        principalTable: "personnels",
                        principalColumn: "IdPers",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ordremission_trajet_Trajetid_trajet",
                        column: x => x.Trajetid_trajet,
                        principalTable: "trajet",
                        principalColumn: "id_trajet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ordrepaiement",
                columns: table => new
                {
                    IdOrdrePayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    totalDeplacement = table.Column<double>(type: "float", nullable: false),
                    tatalKilo = table.Column<double>(type: "float", nullable: false),
                    idOrdremission = table.Column<int>(type: "int", nullable: false),
                    IdOrdreVirement = table.Column<int>(type: "int", nullable: false),
                    OrdreMissionidOrdremission = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordrepaiement", x => x.IdOrdrePayment);
                    table.ForeignKey(
                        name: "FK_ordrepaiement_ordremission_OrdreMissionidOrdremission",
                        column: x => x.OrdreMissionidOrdremission,
                        principalTable: "ordremission",
                        principalColumn: "idOrdremission",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cadres_CorpsIdcorps",
                table: "cadres",
                column: "CorpsIdcorps");

            migrationBuilder.CreateIndex(
                name: "IX_grade_CadreIdCadre",
                table: "grade",
                column: "CadreIdCadre");

            migrationBuilder.CreateIndex(
                name: "IX_ordremission_PersonnelIdPers",
                table: "ordremission",
                column: "PersonnelIdPers");

            migrationBuilder.CreateIndex(
                name: "IX_ordremission_Trajetid_trajet",
                table: "ordremission",
                column: "Trajetid_trajet");

            migrationBuilder.CreateIndex(
                name: "IX_ordrepaiement_OrdreMissionidOrdremission",
                table: "ordrepaiement",
                column: "OrdreMissionidOrdremission");

            migrationBuilder.CreateIndex(
                name: "IX_personnels_CadreIdCadre",
                table: "personnels",
                column: "CadreIdCadre");

            migrationBuilder.CreateIndex(
                name: "IX_personnels_CorpsIdcorps",
                table: "personnels",
                column: "CorpsIdcorps");

            migrationBuilder.CreateIndex(
                name: "IX_personnels_GradesIdgrades",
                table: "personnels",
                column: "GradesIdgrades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ordrepaiement");

            migrationBuilder.DropTable(
                name: "ordrevirement");

            migrationBuilder.DropTable(
                name: "param_paiement");

            migrationBuilder.DropTable(
                name: "param_voiture");

            migrationBuilder.DropTable(
                name: "parametre");

            migrationBuilder.DropTable(
                name: "parametre_img");

            migrationBuilder.DropTable(
                name: "ordremission");

            migrationBuilder.DropTable(
                name: "personnels");

            migrationBuilder.DropTable(
                name: "trajet");

            migrationBuilder.DropTable(
                name: "grade");

            migrationBuilder.DropTable(
                name: "cadres");

            migrationBuilder.DropTable(
                name: "corp");
        }
    }
}
