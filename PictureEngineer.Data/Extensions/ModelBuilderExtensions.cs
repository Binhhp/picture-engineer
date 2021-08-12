
using Microsoft.EntityFrameworkCore;
using PictureEngineer.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace PictureEngineer.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seeder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Quản trị viên",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "b61ac4d3-eeea-4da2-b8cc-864eb375f04e"
                }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2",
                    Name = "Thành viên",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = "bca38043-7fe2-4c02-9e93-e22e37912465"
                }
            );

            modelBuilder.Entity<AspNetUser>().HasData(
                new AspNetUser
                {
                    Id = "8a51ce96-a182-41d3-944f-9addccce84ba",
                    UserName = "binhhp20",
                    NormalizedUserName = "BINHHP20",
                    Email = "binhhp20@gmail.com",
                    NormalizedEmail = "BINHHP20@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEIbHIX20zSBhbwLgoHNk2ZbvWL5R5wQSfPqnepjoBYfYpVhw9o7jIk12i7iDKNlA+A==",
                    SecurityStamp = "6PPOTFQNHXOLMB5IP2NDIA2DDCQPXU4L",
                    ConcurrencyStamp = "a7509d88-e614-45ac-b424-0ec59c25454c",
                    PhoneNumber = "0965788640",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FullName = "Vu Tat Binh",
                    Picture = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/users%2F1596889689_912_Anh-avatar-dep-va-doc-dao-lam-hinh-dai-dien.jpg?alt=media&token=3db2e79f-8701-4ae7-be90-66f7b376735a"

                });


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "8a51ce96-a182-41d3-944f-9addccce84ba"
                });

            modelBuilder.Entity<FAQs>().HasData(
                new FAQs(){
                    Id = 1,
                    Question = "Bạn có yêu cầu bất kỳ phần mềm nào được tải xuống hay hoàn toàn trực tuyến không?",
                    Answer = "Mục tiêu chính đằng sau PEngineer.com là cung cấp trải nghiệm hoàn toàn miễn phí cho người dùng của chúng tôi. Do đó, chúng tôi nhận mọi gánh nặng cuối cùng của chúng tôi. KHÔNG CẦN CÀI ĐẶT BẤT KỲ PHẦN MỀM NÀO trên máy tính xách tay, PC, điện thoại thông minh của bạn, vv .. Điều này hoàn toàn trực tuyến.",
                    ServiceID = 0
                },
                new FAQs(){
                    Id = 2,
                    Question = "Làm thế nào là an toàn để sử dụng cái này?",
                    Answer = "Dữ liệu được truyền bằng HTTPS thông qua mã hóa 256 bit. Tất cả các tệp bạn tải lên máy chủ của chúng tôi cho các hoạt động khác nhau sẽ tự động bị xóa trong vòng một giờ. Bảo mật là ưu tiên hàng đầu của chúng tôi.",
                    ServiceID = 0
                },
                new FAQs(){
                    Id = 3,
                    Question = "Công cụ là miễn phí và không giới hạn?",
                    Answer = "Chúng tôi đã xây dựng công cụ trực tuyến này với mong muốn đóng góp cho cộng đồng. Chúng tôi không có ý định tính phí bất cứ thứ gì từ người dùng.Tuy nhiên, chúng tôi có thể chạy Quảng cáo để hỗ trợ sáng kiến ​​này.",
                    ServiceID = 0
                },
                new FAQs(){
                    Id = 4,
                    Question = "Nhận diện ảnh chụp như thế nào, làm sao để đạt độ chính xác cao?",
                    Answer = "Chúng tôi hiện đang phát triển công cụ và nhận diện tốt nhất. Tuy nhiên, thời gian xử lý và nhận diện vẫn còn bị hạn chế nhưng sẽ được cải thiện nếu bạn chụp ảnh rõ nét, chi tiết xem dưới công cụ.",
                    ServiceID = 0
                },
                new FAQs(){
                    Id = 5,
                    Question = "Bạn có thể nêu chi tiết các hệ điều hành được công cụ hỗ trợ trên máy tính để bàn và PC không?",
                    Answer = "Picture Engineer là một công cụ trực tuyến 100% và hoạt động bên trong trình duyệt web. Vì vậy, không thành vấn đề cho dù bạn đang sử dụng Microsoft Windows, Mac OS, Linux, iOS, Android hay bất kỳ hệ điều hành nào khác. Nếu hệ điều hành của bạn hỗ trợ bất kỳ trình duyệt hiện đại tiêu chuẩn nào như Google Chrome, Firefox, Internet Explorer, Safari, v.v. thì bạn có thể sử dụng công cụ một cách liền mạch.",
                    ServiceID = 0
                },
                new FAQs(){
                    Id = 6,
                    Question = "Công cụ nhận diện là miễn phí và không giới hạn?",
                    Answer = "Hiện tại công cụ là miễn phí và luôn đáp ứng tất cả mọi nhu cầu của người dùng",
                    ServiceID = 1
                },
                new FAQs(){
                    Id = 7,
                    Question = "Có thể nhận diện tất cả các hình ảnh chụp?",
                    Answer = "Hiện tại chúng tôi mới chỉ phát triển nhận diện trên các ảnh chụp tài liệu văn bản nghiên cứu có các yếu tố như bảng, biểu đồ, biểu thức toán học, .... Tương lai chúng tôi sẽ phát triển thêm",
                    ServiceID = 1
                },
                new FAQs(){
                    Id = 8,
                    Question = "Các file ảnh tài liệu của chúng tôi là an toàn?",
                    Answer = "File ảnh được mã hóa base64 khi upload và xử lý sẽ xóa file. Chúng tôi không lưu lại tất cả các file.",
                    ServiceID = 1
                },
                new FAQs(){
                    Id = 9,
                    Question = "Làm cách nào để công cụ nhận diện 100%?",
                    Answer = "Chúng tôi không chắc nhận diện được 100% nhưng sẽ đạt kết quả tốt như ý muốn nếu bạn thực hiện đúng với hướng dẫn bên trên của chúng tôi",
                    ServiceID = 1
                },
                new FAQs(){
                    Id = 10,
                    Question = "Công cụ hỗ trợ ngôn ngữ nhận diện nào?",
                    Answer = "Hiện tại công cụ nhận diện hỗ trợ ngôn ngữ tiếng anh và tiếng việt",
                    ServiceID = 1
                },
                new FAQs()
                {
                    Id = 11,
                    Question = "Tôi có thể lưu một trang PDF không?",
                    Answer = "Có, bạn có thể lưu một hoặc nhiều trang từ một tệp PDF.Bạn có thể tách hoặc trích xuất các trang một cách dễ dàng bằng cách chọn các trang cần thiết từ tệp PDF đã tải lên và tải xuống riêng biệt tất cả cùng một lúc.",
                    ServiceID = 2
                },
                 new FAQs()
                 {
                     Id = 12,
                     Question = "🔥 Làm thế nào để trích xuất các trang từ một pdf?",
                     Answer = "Truy cập công cụ và tải lên tệp PDF mà bạn muốn trích xuất các trang từ đó. Chọn các trang bạn muốn tải xuống và lưu các trang đã chọn. Bạn cũng có thể chia tệp PDF thành hai và duy trì chất lượng và nội dung của tệp PDF gốc.",
                     ServiceID = 2
                 },
                 new FAQs()
                 {
                     Id = 13,
                     Question = "Tôi có bị mất định dạng nội dung khi chuyển đổi từ PDF sang Word không?",
                     Answer = "Không, PictureEngineer là một công cụ tiên tiến cao. Sử dụng công cụ chuyển đổi này, bạn phải yên tâm nhận được định dạng nội dung chính xác sẽ chỉ ở định dạng tệp mong muốn chuyển đổi. Với công cụ của chúng tôi, bạn sẽ không bao giờ bị mất định dạng nội dung gốc, điều này sẽ không bao giờ khiến bạn lo lắng trước khi tải xuống hoặc chia sẻ tệp đã chuyển đổi và mở tệp đó để chỉ cần kiểm tra xem nó có được chuyển đổi đúng cách hay không.",
                     ServiceID = 3
                 },
                 new FAQs()
                 {
                     Id = 14,
                     Question = "Làm thế nào để chuyển đổi một tệp PDF sang Word đúng cách?",
                     Answer = "Chỉ cần ba bước kỳ diệu và bạn đã sẵn sàng để chuyển đổi tệp đúng cách: Bước 1: Tải lên tệp ở định dạng PDF mà bạn muốn chuyển đổi.Bạn có thể tải nó lên từ máy tính, máy tính bảng, máy tính xách tay, điện thoại thông minh hoặc các phương tiện lưu trữ đám mây Bước 2: Bây giờ hãy đợi một vài giây để tải lên và chuyển đổi tệp theo cách thích hợp. Bước 3: Bây giờ bạn có thể tải xuống hoặc chia sẻ tệp một cách rất dễ hiểu.",
                     ServiceID = 3
                 },
                 new FAQs()
                 {
                     Id = 15,
                     Question = "Làm thế nào để chuyển đổi tệp Word mà không có bất kỳ mối đe dọa nào về quyền riêng tư?",
                     Answer = "Picture Engineer sử dụng máy chủ an toàn để cung cấp cho bạn dịch vụ chuyển đổi liền mạch. Nền tảng của chúng tôi an toàn và bảo mật 100% và không có sự can thiệp của bên thứ ba với chúng tôi. Bất kỳ tệp nào bạn tải lên hoặc tải xuống từ công cụ của chúng tôi, không bao giờ được chia sẻ với bất kỳ ai trong bất kỳ điều kiện nào. Chúng tôi hoàn toàn được tin cậy bởi hàng nghìn người dùng hàng ngày và chúng tôi đảm bảo rằng tính riêng tư của tài liệu của bạn luôn được duy trì.",
                     ServiceID = 3
                 }
            );

            modelBuilder.Entity<Services>().HasData(
                new Services
                {
                    Id = 1,
                    Name = "Nhận diện ảnh chụp tài liệu",
                    Description = "Nhận diện và chuyển đổi ảnh chụp văn bản tài liệu sang file docx",
                    Meta = "nhan-dien",
                    
                    UserGuide = "<h2>Cách nhận diện và chuyển đổi ảnh chụp tài liệu văn bản của bạn</h2><ul><li class='answer'><span>Với công cụ của chúng tôi nhận diện với độ chính xác 71.38%. Song để đạt độ chính xác cao nhất, bạn cần lưu ý sau:</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Tải lên file ảnh của bạn từ thiết bị của bạn, cho phép các file ảnh dạng png, jpg, jpeg.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Ảnh chụp tài liệu cần rõ nét, bạn cần chụp ảnh ở giữa màn hình.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Loại bỏ các vật thể không liên quan đến tài liệu cần chụp trong ảnh.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Độ sáng tối vừa phải, dễ nhìn dễ nhận biết.</li></ul>",
                    ImgPath = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_delete-icon.png?alt=media&token=e08dc9de-829a-45c6-b569-c82945af4c89",
                    Icon = "https://s1.iaspire.tech/1/5561/ppt.svg.svg",
                    ParentID = 1,
                    Color = "rgb(248, 80, 196)"
                },
                new Services
                {
                    Id = 2,
                    Name = "Tách PDF",
                    Description = "Chia nhỏ tài liệu PDF lớn thành nhiều tài liệu",
                    Meta = "tach-pdf",
                    UserGuide = "<h2>Cách trích xuất các trang từ tệp PDF của bạn</h2><ul><li><span>< i class='icon-circle-check'></i>&nbsp;&nbsp;Việc chia nhỏ PDF rất tiện lợi và chỉ mất vài phút.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn có thể ✂ chia từng trang PDF thành các tệp riêng lẻ hoặc bạn có thể trích xuất các trang cụ thể bằng cách nhập phạm vi trang đã chọn.</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Chúc mừng! Các tệp PDF của bạn đã sẵn sàng để tải xuống.</li></ul>",
                    ImgPath = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_split-icon.png?alt=media&token=e01f124e-1fc0-4c95-98ee-e48f95da8d66",
                    Icon = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fsplit.png?alt=media&token=58182cef-8307-4141-a46c-c312cd5248aa",
                    ParentID = 2,
                    Color = "#20c997"
                },
                new Services
                {
                    Id = 3,
                    Name = "Chuyển định dạng PDF",
                    Description = "Chuyển PDF sang Word hoặc Excel",
                    Meta = "chuyen-doi-pdf",
                    UserGuide = "<h2>Các bước chuyển đổi PDF sang Docx trên Picture Engineer</h2><ul><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Trước hết, bạn phải giữ cho tệp luôn sẵn sàng trong thiết bị của mình.</span></li><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Sau đó, tải tệp của bạn muốn xử lý lên công cụ của chúng tôi.</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn lựa chọn cách thích hợp và xử lý</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Cuối cùng bạn tải xuống file mới được tạo.</li></ul>",
                    ImgPath = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_pdf-icon.png?alt=media&token=a94b6bf3-3618-4912-a303-cded760e642a",
                    Icon = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fexcel.png?alt=media&token=cf597d9c-497b-4fad-9215-01292087afd7",
                    ParentID = 2,
                    Color = "rgb(255, 46, 94)"
                },
                new Services
                {
                    Id = 4,
                    Name = "Chuyển đổi Docx sang PDF",
                    Description = "Chuyển đổi Docx sang PDF",
                    Meta = "chuyen-doi-docx",
                    UserGuide = "<h2>Các bước chuyển đổi PDF sang Docx trên Picture Engineer</h2><ul><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Trước hết, bạn phải giữ cho tệp luôn sẵn sàng trong thiết bị của mình.</span></li><li><span><i class='icon-circle-check'></i>&nbsp;&nbsp;Sau đó, tải tệp của bạn muốn xử lý lên công cụ của chúng tôi.</span></li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Bạn lựa chọn cách thích hợp và xử lý</li><li><i class='icon-circle-check'></i>&nbsp;&nbsp;Cuối cùng bạn tải xuống file mới được tạo.</li></ul>",
                    ImgPath = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fpageicon_doc-icon.png?alt=media&token=2bb0b3a6-2163-4550-94e9-196291afd34a",
                    Icon = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/services%2Fdoc.png?alt=media&token=a65feb42-5ad1-4757-b0ba-7de1e7379ba5",
                    ParentID = 2,
                    Color = "rgb(70, 134, 248)"
                }
            );

        }
    }
}