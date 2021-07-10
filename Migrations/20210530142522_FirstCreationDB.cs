using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsApi.Migrations
{
    public partial class FirstCreationDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalLicenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Models_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPhone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPhone_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModelClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelClass_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModelClass_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsFromSystem = table.Column<bool>(type: "bit", nullable: false),
                    ModelClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarDetails_ModelClass_ModelClassId",
                        column: x => x.ModelClassId,
                        principalTable: "ModelClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPhoto_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LuggageBoxCapacity = table.Column<double>(type: "float", nullable: true),
                    Clearance = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Wheelbase = table.Column<double>(type: "float", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dimension_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exterior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrimSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RainSensor = table.Column<bool>(type: "bit", nullable: true),
                    AutoFoldingMirror = table.Column<bool>(type: "bit", nullable: true),
                    PanoramaRoof = table.Column<bool>(type: "bit", nullable: true),
                    LedDaytimeRunningLamps = table.Column<bool>(type: "bit", nullable: true),
                    Headlamps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FogLambs = table.Column<bool>(type: "bit", nullable: true),
                    AutoLighting = table.Column<bool>(type: "bit", nullable: true),
                    RearLambs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SunRoof = table.Column<bool>(type: "bit", nullable: true),
                    WheelsWithTireSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LightSensors = table.Column<bool>(type: "bit", nullable: true),
                    PowerMirrors = table.Column<bool>(type: "bit", nullable: true),
                    PrivacyGlass = table.Column<bool>(type: "bit", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exterior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exterior_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterLock = table.Column<bool>(type: "bit", nullable: true),
                    RearParkingSensors = table.Column<bool>(type: "bit", nullable: true),
                    AmbientLighting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectronicWindow = table.Column<bool>(type: "bit", nullable: true),
                    DriveModeSelect = table.Column<int>(type: "int", nullable: true),
                    LeatherTransmission = table.Column<bool>(type: "bit", nullable: true),
                    VariableHeatedDriverPassengerSeat = table.Column<bool>(type: "bit", nullable: true),
                    MovableSteeringWheel = table.Column<bool>(type: "bit", nullable: true),
                    RearViewCamera = table.Column<bool>(type: "bit", nullable: true),
                    KeylessStartStop = table.Column<bool>(type: "bit", nullable: true),
                    BackHolder = table.Column<bool>(type: "bit", nullable: true),
                    AutoDimmingMirror = table.Column<bool>(type: "bit", nullable: true),
                    AutoFoldingBackSeats = table.Column<bool>(type: "bit", nullable: true),
                    Ac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dashboard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontCamera = table.Column<bool>(type: "bit", nullable: true),
                    PassengerSeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeatherSteeringWheel = table.Column<bool>(type: "bit", nullable: true),
                    PaddleShifters = table.Column<bool>(type: "bit", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: true),
                    PowerTailgate = table.Column<bool>(type: "bit", nullable: true),
                    AcBackSeats = table.Column<bool>(type: "bit", nullable: true),
                    DriverSeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontParkingSensors = table.Column<bool>(type: "bit", nullable: true),
                    KeylessEntry = table.Column<bool>(type: "bit", nullable: true),
                    LeatherSeats = table.Column<bool>(type: "bit", nullable: true),
                    MultiFunction = table.Column<bool>(type: "bit", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interior_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Multimedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WirlessCharger = table.Column<bool>(type: "bit", nullable: true),
                    Bluetooth = table.Column<bool>(type: "bit", nullable: true),
                    SmartphoneLinkSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Touchscreen = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Usb = table.Column<bool>(type: "bit", nullable: true),
                    MultifunctionSteeringWheel = table.Column<bool>(type: "bit", nullable: true),
                    CdPlayer = table.Column<bool>(type: "bit", nullable: true),
                    Speakers = table.Column<bool>(type: "bit", nullable: true),
                    NavigationSystem = table.Column<bool>(type: "bit", nullable: true),
                    Aux = table.Column<bool>(type: "bit", nullable: true),
                    HeadUpDisplay = table.Column<bool>(type: "bit", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multimedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multimedia_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cylinders = table.Column<int>(type: "int", nullable: true),
                    FuelTankCapacity = table.Column<double>(type: "float", nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: true),
                    MaxToruqe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelConsumption = table.Column<double>(type: "float", nullable: true),
                    GearShifts = table.Column<int>(type: "int", nullable: true),
                    Acceleration = table.Column<double>(type: "float", nullable: true),
                    MaxPower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeed = table.Column<double>(type: "float", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performance_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Safety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TractionControl = table.Column<bool>(type: "bit", nullable: true),
                    ElectroncStabilityControl = table.Column<bool>(type: "bit", nullable: true),
                    AntiLockBrakingSystem = table.Column<bool>(type: "bit", nullable: true),
                    TirePressureMonitoring = table.Column<bool>(type: "bit", nullable: true),
                    SpeedLimiter = table.Column<bool>(type: "bit", nullable: true),
                    AutoStartStopFunctions = table.Column<bool>(type: "bit", nullable: true),
                    PowerAssistedSteering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immobilizer = table.Column<bool>(type: "bit", nullable: true),
                    ElectronicBrakeForceDistribution = table.Column<bool>(type: "bit", nullable: true),
                    ChildSeats = table.Column<bool>(type: "bit", nullable: true),
                    HillAssist = table.Column<bool>(type: "bit", nullable: true),
                    Airbags = table.Column<int>(type: "int", nullable: true),
                    ActiveParkAssist = table.Column<bool>(type: "bit", nullable: true),
                    ElectricHandbrake = table.Column<bool>(type: "bit", nullable: true),
                    CruiseControl = table.Column<bool>(type: "bit", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safety", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Safety_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarLicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CarDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCar_CarDetails_CarDetailsId",
                        column: x => x.CarDetailsId,
                        principalTable: "CarDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCar_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellingData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Km = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgencyMaintainance = table.Column<bool>(type: "bit", nullable: true),
                    Guarntee = table.Column<bool>(type: "bit", nullable: true),
                    Fabrique = table.Column<bool>(type: "bit", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellingData_UserCar_UserCarId",
                        column: x => x.UserCarId,
                        principalTable: "UserCar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_ModelClassId",
                table: "CarDetails",
                column: "ModelClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhoto_CarDetailsId",
                table: "CarPhoto",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Dimension_CarDetailsId",
                table: "Dimension",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exterior_CarDetailsId",
                table: "Exterior",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Interior_CarDetailsId",
                table: "Interior",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelClass_ClassId",
                table: "ModelClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelClass_ModelId",
                table: "ModelClass",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_TypeId",
                table: "Models",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Multimedia_CarDetailsId",
                table: "Multimedia",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_CarDetailsId",
                table: "Performance",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Safety_CarDetailsId",
                table: "Safety",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SellingData_UserCarId",
                table: "SellingData",
                column: "UserCarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCar_CarDetailsId",
                table: "UserCar",
                column: "CarDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCar_UserId",
                table: "UserCar",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhone_UserId",
                table: "UserPhone",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarPhoto");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "Exterior");

            migrationBuilder.DropTable(
                name: "Interior");

            migrationBuilder.DropTable(
                name: "Multimedia");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "Safety");

            migrationBuilder.DropTable(
                name: "SellingData");

            migrationBuilder.DropTable(
                name: "UserPhone");

            migrationBuilder.DropTable(
                name: "UserCar");

            migrationBuilder.DropTable(
                name: "CarDetails");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ModelClass");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
