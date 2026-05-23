using ProxiWorkAPI.Data;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (_context.Users.Any()) return;

            var admin = new User
            {
                FullName = "أدمن النظام",
                PhoneNumber = "01000000000",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.Admin,
                Latitude = 30.0444,
                Longitude = 31.2357,
                City = "القاهرة",
                IsActive = true
            };
            var employer1 = new User
            {
                FullName = "محمود صاحب المطعم",
                PhoneNumber = "01111111111",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.Employer,
                Latitude = 30.0500,
                Longitude = 31.2400,
                City = "القاهرة",
                IsActive = true
            };
            var employer2 = new User
            {
                FullName = "سارة صاحبة المحل",
                PhoneNumber = "01155555555",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.Employer,
                Latitude = 30.0600,
                Longitude = 31.2500,
                City = "القاهرة",
                IsActive = true
            };
            var seeker1 = new User
            {
                FullName = "أحمد الباحث عن عمل",
                PhoneNumber = "01222222222",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.JobSeeker,
                Latitude = 30.0480,
                Longitude = 31.2380,
                City = "القاهرة",
                IsActive = true
            };
            var seeker2 = new User
            {
                FullName = "منى طالبة شغل",
                PhoneNumber = "01333333333",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.JobSeeker,
                Latitude = 30.0520,
                Longitude = 31.2420,
                City = "القاهرة",
                IsActive = true
            };
            var seeker3 = new User
            {
                FullName = "كريم مهندس",
                PhoneNumber = "01444444444",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = UserType.JobSeeker,
                Latitude = 30.0550,
                Longitude = 31.2450,
                City = "القاهرة",
                IsActive = true
            };

            _context.Users.AddRange(admin, employer1, employer2,
                                    seeker1, seeker2, seeker3);
            _context.SaveChanges();
            var job1 = new Job
            {
                Title = "كاشير",
                Description = "مطلوب كاشير لسوبر ماركت خبرة سنة",
                Salary = 5000,
                WorkingHours = "9AM - 5PM",
                RequiredSkills = "أمانة، كمبيوتر",
                City = "القاهرة",
                RadiusKm = 5,
                VacanciesCount = 2,
                Latitude = 30.0490,
                Longitude = 31.2390,
                EmployerId = employer1.Id,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };
            var job2 = new Job
            {
                Title = "مندوب توصيل",
                Description = "مطلوب مندوب برخصة قيادة سارية",
                Salary = 6000,
                WorkingHours = "10AM - 6PM",
                RequiredSkills = "رخصة قيادة، معرفة الطرق",
                City = "القاهرة",
                RadiusKm = 10,
                VacanciesCount = 3,
                Latitude = 30.0510,
                Longitude = 31.2410,
                EmployerId = employer1.Id,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };
            var job3 = new Job
            {
                Title = "موظف استقبال",
                Description = "مطلوب موظف استقبال لعيادة طبية",
                Salary = 4500,
                WorkingHours = "2PM - 10PM",
                RequiredSkills = "لباقة، إنجليزي، كمبيوتر",
                City = "القاهرة",
                RadiusKm = 5,
                VacanciesCount = 1,
                Latitude = 30.0560,
                Longitude = 31.2460,
                EmployerId = employer2.Id,
                ExpiresAt = DateTime.UtcNow.AddMonths(2),
                IsActive = true
            };
            var job4 = new Job
            {
                Title = "بائع ملابس",
                Description = "مطلوب بائع لمحل ملابس وسط البلد",
                Salary = 4000,
                WorkingHours = "11AM - 9PM",
                RequiredSkills = "تعامل مع العملاء",
                City = "القاهرة",
                RadiusKm = 3,
                VacanciesCount = 2,
                Latitude = 30.0440,
                Longitude = 31.2360,
                EmployerId = employer2.Id,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };
            var job5 = new Job
            {
                Title = "طباخ مشويات",
                Description = "مطلوب طباخ محترف خبرة 3 سنين",
                Salary = 7000,
                WorkingHours = "12PM - 12AM",
                RequiredSkills = "خبرة مشويات 3 سنين",
                City = "القاهرة",
                RadiusKm = 8,
                VacanciesCount = 1,
                Latitude = 30.0530,
                Longitude = 31.2430,
                EmployerId = employer1.Id,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };

            _context.Jobs.AddRange(job1, job2, job3, job4, job5);
            _context.SaveChanges();
            var applications = new List<Application>
            {
                new Application
                {
                    ApplicantId = seeker1.Id, JobId = job1.Id,
                    Status = ApplicationStatus.Pending,
                    CvImageUrl = "ahmed_cv.jpg",
                    CoverMessage = "عندي خبرة سنتين وبحب الشغل"
                },
                new Application
                {
                    ApplicantId = seeker2.Id, JobId = job1.Id,
                    Status = ApplicationStatus.Accepted,
                    CvImageUrl = "mona_cv.jpg",
                    CoverMessage = "عندي خبرة في المبيعات"
                },
                new Application
                {
                    ApplicantId = seeker1.Id, JobId = job2.Id,
                    Status = ApplicationStatus.Pending,
                    CvImageUrl = "ahmed_cv.jpg",
                    CoverMessage = "عندي رخصة وخبرة توصيل"
                },
                new Application
                {
                    ApplicantId = seeker3.Id, JobId = job3.Id,
                    Status = ApplicationStatus.Rejected,
                    CvImageUrl = "karim_cv.jpg",
                    CoverMessage = "بتكلم إنجليزي كويس"
                }
            };

            _context.Applications.AddRange(applications);
            _context.SaveChanges();

            // ── Notifications ──────────────────────────
            var notifications = new List<Notification>
            {
                new Notification
                {
                    UserId = seeker1.Id,
                    Message = "تم استلام طلبك على وظيفة كاشير",
                    IsRead = false, Type = "application_status"
                },
                new Notification
                {
                    UserId = seeker2.Id,
                    Message = "مبروك! تم قبول طلبك على وظيفة كاشير 🎉",
                    IsRead = false, Type = "application_status"
                },
                new Notification
                {
                    UserId = employer1.Id,
                    Message = "في متقدم جديد على وظيفة كاشير",
                    IsRead = false, Type = "new_application"
                },
                new Notification
                {
                    UserId = seeker1.Id,
                    Message = "وظيفة مندوب توصيل قريبة منك على بعد 500 متر!",
                    IsRead = true, Type = "job_nearby"
                },
                new Notification
                {
                    UserId = seeker3.Id,
                    Message = "للأسف لم يتم قبول طلبك على وظيفة موظف استقبال",
                    IsRead = false, Type = "application_status"
                }
            };

            _context.Notifications.AddRange(notifications);
            _context.SaveChanges();
        }
    }
}