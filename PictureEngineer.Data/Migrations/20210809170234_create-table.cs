using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureEngineer.Data.Migrations
{
    public partial class createtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLOG",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    Contents = table.Column<string>(nullable: false),
                    MetaTitle = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLOG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    ServiceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FILES",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Meta = table.Column<string>(nullable: true),
                    UserGuide = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROLE_CLAIMS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_CLAIMS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_LOGINS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_USER_LOGINS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLES",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_USER_ROLES_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_ROLES_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_TOKENS",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_TOKENS", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_USER_TOKENS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FAQS",
                columns: new[] { "Id", "Answer", "Question", "ServiceID" },
                values: new object[,]
                {
                    { 8, "File ảnh được mã hóa base64 khi upload và xử lý sẽ xóa file. Chúng tôi không lưu lại tất cả các file.", "Các file ảnh tài liệu của chúng tôi là an toàn?", 1 },
                    { 15, "Picture Engineer sử dụng máy chủ an toàn để cung cấp cho bạn dịch vụ chuyển đổi liền mạch. Nền tảng của chúng tôi an toàn và bảo mật 100% và không có sự can thiệp của bên thứ ba với chúng tôi. Bất kỳ tệp nào bạn tải lên hoặc tải xuống từ công cụ của chúng tôi, không bao giờ được chia sẻ với bất kỳ ai trong bất kỳ điều kiện nào. Chúng tôi hoàn toàn được tin cậy bởi hàng nghìn người dùng hàng ngày và chúng tôi đảm bảo rằng tính riêng tư của tài liệu của bạn luôn được duy trì.", "Làm thế nào để chuyển đổi tệp Word mà không có bất kỳ mối đe dọa nào về quyền riêng tư?", 3 },
                    { 14, "Chỉ cần ba bước kỳ diệu và bạn đã sẵn sàng để chuyển đổi tệp đúng cách: Bước 1: Tải lên tệp ở định dạng PDF mà bạn muốn chuyển đổi.Bạn có thể tải nó lên từ máy tính, máy tính bảng, máy tính xách tay, điện thoại thông minh hoặc các phương tiện lưu trữ đám mây Bước 2: Bây giờ hãy đợi một vài giây để tải lên và chuyển đổi tệp theo cách thích hợp. Bước 3: Bây giờ bạn có thể tải xuống hoặc chia sẻ tệp một cách rất dễ hiểu.", "Làm thế nào để chuyển đổi một tệp PDF sang Word đúng cách?", 3 },
                    { 1, "Mục tiêu chính đằng sau PEngineer.com là cung cấp trải nghiệm hoàn toàn miễn phí cho người dùng của chúng tôi. Do đó, chúng tôi nhận mọi gánh nặng cuối cùng của chúng tôi. KHÔNG CẦN CÀI ĐẶT BẤT KỲ PHẦN MỀM NÀO trên máy tính xách tay, PC, điện thoại thông minh của bạn, vv .. Điều này hoàn toàn trực tuyến.", "Bạn có yêu cầu bất kỳ phần mềm nào được tải xuống hay hoàn toàn trực tuyến không?", 0 },
                    { 2, "Dữ liệu được truyền bằng HTTPS thông qua mã hóa 256 bit. Tất cả các tệp bạn tải lên máy chủ của chúng tôi cho các hoạt động khác nhau sẽ tự động bị xóa trong vòng một giờ. Bảo mật là ưu tiên hàng đầu của chúng tôi.", "Làm thế nào là an toàn để sử dụng cái này?", 0 },
                    { 3, "Chúng tôi đã xây dựng công cụ trực tuyến này với mong muốn đóng góp cho cộng đồng. Chúng tôi không có ý định tính phí bất cứ thứ gì từ người dùng.Tuy nhiên, chúng tôi có thể chạy Quảng cáo để hỗ trợ sáng kiến ​​này.", "Công cụ là miễn phí và không giới hạn?", 0 },
                    { 4, "Chúng tôi hiện đang phát triển công cụ và nhận diện tốt nhất. Tuy nhiên, thời gian xử lý và nhận diện vẫn còn bị hạn chế nhưng sẽ được cải thiện nếu bạn chụp ảnh rõ nét, chi tiết xem dưới công cụ.", "Nhận diện ảnh chụp như thế nào, làm sao để đạt độ chính xác cao?", 0 },
                    { 5, "Picture Engineer là một công cụ trực tuyến 100% và hoạt động bên trong trình duyệt web. Vì vậy, không thành vấn đề cho dù bạn đang sử dụng Microsoft Windows, Mac OS, Linux, iOS, Android hay bất kỳ hệ điều hành nào khác. Nếu hệ điều hành của bạn hỗ trợ bất kỳ trình duyệt hiện đại tiêu chuẩn nào như Google Chrome, Firefox, Internet Explorer, Safari, v.v. thì bạn có thể sử dụng công cụ một cách liền mạch.", "Bạn có thể nêu chi tiết các hệ điều hành được công cụ hỗ trợ trên máy tính để bàn và PC không?", 0 },
                    { 6, "Hiện tại công cụ là miễn phí và luôn đáp ứng tất cả mọi nhu cầu của người dùng", "Công cụ nhận diện là miễn phí và không giới hạn?", 1 },
                    { 7, "Hiện tại chúng tôi mới chỉ phát triển nhận diện trên các ảnh chụp tài liệu văn bản nghiên cứu có các yếu tố như bảng, biểu đồ, biểu thức toán học, .... Tương lai chúng tôi sẽ phát triển thêm", "Có thể nhận diện tất cả các hình ảnh chụp?", 1 },
                    { 13, "Không, PictureEngineer là một công cụ tiên tiến cao. Sử dụng công cụ chuyển đổi này, bạn phải yên tâm nhận được định dạng nội dung chính xác sẽ chỉ ở định dạng tệp mong muốn chuyển đổi. Với công cụ của chúng tôi, bạn sẽ không bao giờ bị mất định dạng nội dung gốc, điều này sẽ không bao giờ khiến bạn lo lắng trước khi tải xuống hoặc chia sẻ tệp đã chuyển đổi và mở tệp đó để chỉ cần kiểm tra xem nó có được chuyển đổi đúng cách hay không.", "Tôi có bị mất định dạng nội dung khi chuyển đổi từ PDF sang Word không?", 3 },
                    { 9, "Chúng tôi không chắc nhận diện được 100% nhưng sẽ đạt kết quả tốt như ý muốn nếu bạn thực hiện đúng với hướng dẫn bên trên của chúng tôi", "Làm cách nào để công cụ nhận diện 100%?", 1 },
                    { 10, "Hiện tại công cụ nhận diện hỗ trợ ngôn ngữ tiếng anh và tiếng việt", "Công cụ hỗ trợ ngôn ngữ nhận diện nào?", 1 },
                    { 11, "Có, bạn có thể lưu một hoặc nhiều trang từ một tệp PDF.Bạn có thể tách hoặc trích xuất các trang một cách dễ dàng bằng cách chọn các trang cần thiết từ tệp PDF đã tải lên và tải xuống riêng biệt tất cả cùng một lúc.", "Tôi có thể lưu một trang PDF không?", 2 },
                    { 12, "Truy cập công cụ và tải lên tệp PDF mà bạn muốn trích xuất các trang từ đó. Chọn các trang bạn muốn tải xuống và lưu các trang đã chọn. Bạn cũng có thể chia tệp PDF thành hai và duy trì chất lượng và nội dung của tệp PDF gốc.", "🔥 Làm thế nào để trích xuất các trang từ một pdf?", 2 }
                });

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "b61ac4d3-eeea-4da2-b8cc-864eb375f04e", "Quản trị viên", "ADMIN" },
                    { "2", "bca38043-7fe2-4c02-9e93-e22e37912465", "Thành viên", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "SERVICES",
                columns: new[] { "Id", "Color", "Description", "Icon", "ImgPath", "Meta", "Name", "ParentID", "UserGuide" },
                values: new object[,]
                {
                    { 1, "rgb(248, 80, 196)", "Nhận diện và chuyển đổi ảnh chụp văn bản tài liệu sang file docx", "https://s1.iaspire.tech/1/5561/ppt.svg.svg", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_delete-icon.png?alt=media&token=e08dc9de-829a-45c6-b569-c82945af4c89", "nhan-dien", "Nhận diện ảnh chụp tài liệu", 1, "<h2>Cách nhận diện và chuyển đổi ảnh chụp tài liệu văn bản của bạn</h2><ul><li class='answer'><span>Với công cụ của chúng tôi nhận diện với độ chính xác 71.38%. Song để đạt độ chính xác cao nhất, bạn cần lưu ý sau:</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Tải lên file ảnh của bạn từ thiết bị của bạn, cho phép các file ảnh dạng png, jpg, jpeg.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Ảnh chụp tài liệu cần rõ nét, bạn cần chụp ảnh ở giữa màn hình.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Loại bỏ các vật thể không liên quan đến tài liệu cần chụp trong ảnh.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Độ sáng tối vừa phải, dễ nhìn dễ nhận biết.</li></ul>" },
                    { 2, "#20c997", "Chia nhỏ tài liệu PDF lớn thành nhiều tài liệu", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fsplit.png?alt=media&token=58182cef-8307-4141-a46c-c312cd5248aa", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_split-icon.png?alt=media&token=e01f124e-1fc0-4c95-98ee-e48f95da8d66", "tach-pdf", "Tách PDF", 2, "<h2>Cách trích xuất các trang từ tệp PDF của bạn</h2><ul><li><span>< i class='icon-circle-check'></i>&nbsp;&nbsp;Việc chia nhỏ PDF rất tiện lợi và chỉ mất vài phút.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn có thể ✂ chia từng trang PDF thành các tệp riêng lẻ hoặc bạn có thể trích xuất các trang cụ thể bằng cách nhập phạm vi trang đã chọn.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Chúc mừng! Các tệp PDF của bạn đã sẵn sàng để tải xuống.</li></ul>" },
                    { 3, "rgb(255, 46, 94)", "Chuyển PDF sang Word hoặc Excel", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fexcel.png?alt=media&token=cf597d9c-497b-4fad-9215-01292087afd7", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_pdf-icon.png?alt=media&token=a94b6bf3-3618-4912-a303-cded760e642a", "chuyen-doi-pdf", "Chuyển định dạng PDF", 2, "<h2>Các bước chuyển đổi PDF sang Docx trên Picture Engineer</h2><ul><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Trước hết, bạn phải giữ cho tệp luôn sẵn sàng trong thiết bị của mình.</span></li><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Sau đó, tải tệp của bạn muốn xử lý lên công cụ của chúng tôi.</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn lựa chọn cách thích hợp và xử lý</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Cuối cùng bạn tải xuống file mới được tạo.</li></ul>" },
                    { 4, "rgb(70, 134, 248)", "Chuyển đổi Docx sang PDF", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fdoc.png?alt=media&token=a65feb42-5ad1-4757-b0ba-7de1e7379ba5", "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_doc-icon.png?alt=media&token=2bb0b3a6-2163-4550-94e9-196291afd34a", "chuyen-doi-docx", "Chuyển đổi Docx sang PDF", 2, "<h2>Các bước chuyển đổi PDF sang Docx trên Picture Engineer</h2><ul><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Trước hết, bạn phải giữ cho tệp luôn sẵn sàng trong thiết bị của mình.</span></li><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Sau đó, tải tệp của bạn muốn xử lý lên công cụ của chúng tôi.</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn lựa chọn cách thích hợp và xử lý</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Cuối cùng bạn tải xuống file mới được tạo.</li></ul>" }
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8a51ce96-a182-41d3-944f-9addccce84ba", 0, "a7509d88-e614-45ac-b424-0ec59c25454c", "binhhp20@gmail.com", true, "Vu Tat Binh", true, null, "BINHHP20@GMAIL.COM", "BINHHP20", "AQAAAAEAACcQAAAAEIbHIX20zSBhbwLgoHNk2ZbvWL5R5wQSfPqnepjoBYfYpVhw9o7jIk12i7iDKNlA+A==", "0965788640", false, "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/users%2F1596889689_912_Anh-avatar-dep-va-doc-dao-lam-hinh-dai-dien.jpg?alt=media&token=3db2e79f-8701-4ae7-be90-66f7b376735a", "6PPOTFQNHXOLMB5IP2NDIA2DDCQPXU4L", false, "binhhp20" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8a51ce96-a182-41d3-944f-9addccce84ba", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CLAIMS_RoleId",
                table: "ROLE_CLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ROLES",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_CLAIMS_UserId",
                table: "USER_CLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGINS_UserId",
                table: "USER_LOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_RoleId",
                table: "USER_ROLES",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USERS",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USERS",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLOG");

            migrationBuilder.DropTable(
                name: "FAQS");

            migrationBuilder.DropTable(
                name: "FILES");

            migrationBuilder.DropTable(
                name: "ROLE_CLAIMS");

            migrationBuilder.DropTable(
                name: "SERVICES");

            migrationBuilder.DropTable(
                name: "USER_CLAIMS");

            migrationBuilder.DropTable(
                name: "USER_LOGINS");

            migrationBuilder.DropTable(
                name: "USER_ROLES");

            migrationBuilder.DropTable(
                name: "USER_TOKENS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
