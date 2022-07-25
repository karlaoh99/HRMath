using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HRMath.Data;
using HRMath.Models;
using System.IO;
using HRMath.Infraestructure;


namespace HRMath
{
    public class Startup
    {

        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Setting up the human resources database connection
            services.AddDbContext<EFDatabaseContext>(options=>
                options.UseSqlServer(
                    Configuration["ConnectionStrings:HRConnection"].Replace("%CONTENTROOTPATH%", _env.ContentRootPath))
            );

            //Setting up the identity database connection
            services.AddDbContext<AppIdentityDbContext>(options=>
                options.UseSqlServer(
                    Configuration["ConnectionStrings:IdentityConnection"].Replace("%CONTENTROOTPATH%",_env.ContentRootPath ))
            );

            services.AddTransient<IProfessorRepository, ProfessorRepositoryEF>();
            services.AddTransient<IClassRepository, ClassRepositoryEF>();
            services.AddTransient<IScheduleRepository, ScheduleRepositoryEF>();
            services.AddTransient<ISubjectRepository, EFSubjectRepository>();
            services.AddTransient<IGradeRepository, EFGradeRepository>();
            services.AddTransient<IFacultyRepository, EFFacultyRepository>();

            //Defines the classes that will represent users and roles
            services.AddIdentity<AppUser,IdentityRole>(opts =>{
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = false;
                opts.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            
            services.ConfigureApplicationCookie(opts => {
                opts.LoginPath = "/Account/Login";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            //SeedDatabase(services).Wait();
            //TestProfessors(services);
            //TestClasses(services);
            //SeedSubject(services);
        }
        
        public void TestClasses(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EFDatabaseContext>();
            var alg = new Subject{ Name="Algebra", IsAnual=false, IsOptative=false};
            var anl = new Subject{ Name="Analisis", IsAnual=false, IsOptative=false};
            var pro = new Subject{ Name="Programacion", IsAnual=false, IsOptative=false}; 
            context.Subject.Add(alg);
            context.Subject.Add(anl);
            context.Subject.Add(pro);
            var sche = new Schedule {Name="Horario Sem Impar"};
            context.Schedule.Add(sche);
            var first = new Class{ Classroom=1, DayOfWeek="Monday", HourInit=new TimeSpan(9,0,0), HourFinish=new TimeSpan(10,30,0), IdSubjectNavigation=anl , IdM=1, IdScheduleNavigation=sche};
            context.Class.Add(first);

            context.SaveChanges();

        }

        public void TestProfessors(IServiceProvider serviceProvider) 
        {
            var context = serviceProvider.GetRequiredService<EFDatabaseContext>();

            // string[] names = new string[] { "Alejandro", "Alejandra", "Alberto", "Alexander", "Amanda", "Amalia", "Betty", "Carlos", "Camila", "Damian", "Fernando", "Gerardo", "Hilda", "Juan", "Luis", "Mario", "Marcos", "Rodrigo", "Victor" };
            // string[] lastNames = new string[] { "Alonso", "Borgia", "Castell", "Pardo", "Prado", "Rodriguez", "Martínez", "Pérez", "Gutierrez", "Díaz", "López", "Castillo", "Jimenez" };

            // Dictionary<int, int> months = new Dictionary<int, int> { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };

            // //Professor[] profs = new Professor[] {
            // //new Professor {PersonalId =, Name=, Email=, Address=,ScientificGrade=,TeachingCategory= };
            // //};

            // Random r = new Random();
            // List<string> professorsCons = new List<string>();
            // for (int i = 0; i < names.Length; i++)
            //         {
            //             var y = r.Next(80, 96);
            //             var m = r.Next(1, 13);
            //             var d = r.Next(1, months[m]+1);
            //             var id = 10000 - i;

            //             string year = y.ToString();
            //             string month = m < 10 ? "0" + m.ToString() : m.ToString();
            //             string day = d < 10 ? "0" + d.ToString() : d.ToString();

            //             string iden = id.ToString();
            //             while (iden.Length < 5)
            //                 iden = "0" + iden;

            //             string ci = year + month + day + iden;
            //             int fln = r.Next(lastNames.Length);
            //             int sln = r.Next(lastNames.Length);
            //             string name = $"{names[i]} {lastNames[fln]} {lastNames[sln]}";
            //             string sg = Nomenclators.ScientificCategories[r.Next(0, 4)];
            //             string tc = Nomenclators.TeachingCategories[r.Next(0, 5)];
            //             string email = $"{names[i]}{lastNames[fln]}{lastNames[sln]}".ToLower();
            //             var p = $"new Professor {{PersonalId=\"{ci}\", Name=\"{name}\", Email=\"{email}@example.com\", Address=\"Calle N #{i} e/ San Lázaro y Jovellar\",ScientificGrade=\"{sg}\",TeachingCategory=\"{tc}\" }}";
            //             professorsCons.Add(p);


            //         }

            // Console.WriteLine(string.Join(",\n", professorsCons));


            
            
            // context.Subject.Add(new Subject{ Name="Analisis", IsAnual=false, IsOptative=false});
            context.Professor.AddRange(Seeds.Professors);
            context.SaveChanges();
        }


        public void SeedSubject(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EFDatabaseContext>();

            var matcom = new Faculty { Name = "Matematica y Computacion", Address = "123" };            
            var biologia = new Faculty { Name = "Biologia", Address = "456" };
            
            var comp = new Grade { Name = "Ciencias de la Computacion", IdFNavigation = matcom };
            var math = new Grade { Name = "Matematica", IdFNavigation = matcom };
            var bio = new Grade { Name = "Biologia", IdFNavigation = biologia };            

            var alg = new Subject { Name = "Algebra", IsAnual = false, IsOptative = false };
            var anal = new Subject { Name = "Analisis Matematico", IsAnual = false, IsOptative = false };
            var pro = new Subject { Name = "Programacion", IsAnual = true, IsOptative = false };

            var comp_alg = new SubjectGrade { IdGNavigation = comp, IdSNavigation = alg };
            var comp_anal = new SubjectGrade { IdGNavigation = comp, IdSNavigation = anal };
            var comp_pro = new SubjectGrade { IdGNavigation = comp, IdSNavigation = pro };

            matcom.Grade.Add(comp);
            matcom.Grade.Add(math);
            biologia.Grade.Add(bio);
            context.Faculty.Add(matcom);
            context.Faculty.Add(biologia);

            comp.SubjectGrade.Add(comp_alg);
            comp.SubjectGrade.Add(comp_anal);
            comp.SubjectGrade.Add(comp_pro);
            context.Grade.Add(comp);
            context.Grade.Add(math);
            context.Grade.Add(bio);

            alg.SubjectGrade.Add(comp_alg);
            anal.SubjectGrade.Add(comp_anal);
            pro.SubjectGrade.Add(comp_pro);
            context.Subject.Add(alg);
            context.Subject.Add(anal);
            context.Subject.Add(pro);

            context.SaveChanges();
        }
        public async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            var _identityContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            bool created = _identityContext.Database.EnsureCreated();

            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            AppUser[] admins = new AppUser[] { new AppUser {UserName = "Victor", Email = "victor@example.com"},
                                                new AppUser {UserName = "Karla", Email = "karla@example.com"},
                                                new AppUser {UserName = "Amanda", Email = "amanda@example.com"}};
            
            await _roleManager.CreateAsync(new IdentityRole("Admin"));                
            await _roleManager.CreateAsync(new IdentityRole("User"));

            foreach (var a in admins){
                await _userManager.CreateAsync(a, "password");
                await _userManager.AddToRoleAsync(a, "Admin");
            }
        }


    }
}
