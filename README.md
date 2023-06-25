# aspnet-b8-shamimhosen
Asp.Net Batch-8 Main Repository which is used for Class Task(Assignment, Exam, Project)


## [Class-10 (New Syllabus & SASS)](https://docs.google.com/document/d/1A9cbTsqpL61j4-cdMniQUc9Eqo92qgTaHyxadCoJ4oU/edit) 
* New Syllabus (30)
* Injection Type(61)--Constractor, Property, Methode Dependency Injection--Constractor most populler
* SCSS File Complie(82) 
* We can nest style:(95)
* Split/Import File:(97)
* Mixin:(100)
* OMR(115) -- ado.net, entity framework, dapper, inhybernet
## Class-30 Identity Framwork
1. Add IdentityDbContext(30) & paramiter(55)
   <details>
    <summary>ApplicationDbContext</summary>
    
    ```c#
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>, IApplicationDbContext
    {     }
    ```
   </details>
2. Add Membership class-31\
   ![image](https://github.com/Shamim448/aspnet-b8-shamimhosen/assets/43339514/37cd35f6-003c-4892-8174-2388a90cb733)
3. Modify ServiceCollectionExtention> AddIdentity-37\
   আমরা প্রোগ্রাম ডট সিএস থেকে যে আইডেন্টিটি ডিফল্ট যে মেথডটা নিয়ে এসে একটা একটা সার্ভিস কালেকশন তৈরি করেছি সেখানে যে ম্যাপটা তৈরি করেছি staticদিয়ে তার ভিতরে কিছু কনফিগারেশন 
   আমাদের দিয়ে দিতে হবে. যার ভিতরে বিভিন্ন ধরনের লগইন logout এই পাথরগুলো থাকবে এবং কুকি থাকবে তারপরে গিয়ে পাসওয়ার্ড সেটিং থাকবে লক আউট সেটিং থাকবে এই টাইপের কোড 
   এবং নিচে সার্ভিস ডট রেজাল্ট পেজ এটা দিয়ে দিতে হবে services.AddRazorPages();
   <details>
    <summary>ServiceCollectionExtention</summary>
    
    ```c#
    namespace Crud.Persistance.Extentions
    {
    public static class ServiceCollectionExtentions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.Cookie.Name = "FirstDemoPortal.Identity";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddRazorPages();
        }
      }
    }
    ```
   </details>
4. Edit _LoginPartial-43\
   যখন আমরা উপরের কাজগুলো করব তখন লগ ইন পার্শিয়াল এর মধ্যে আইডেন্টিটি ইউজার থাকবে সেটা চেঞ্জ করে যেহেতু
   আমরা আইডেন্টিটি ইউজারের পরিবর্তে অ্যাপ্লিকেশন ইউজার ক্লাস তৈরি করেছি 
   সুতরাং সেখানেও আইডেন্টিটি টিউজারের পরিবর্তে অ্যাপ্লিকেশন user দিয়ে দিতে হবে
   <details>
    <summary>Dummy</summary>
    
    ```c#
    @using Crud.Persistance.Features.Membership;
    @using Microsoft.AspNetCore.Identity
    @inject SignInManager<ApplicationUser> SignInManager
    @inject UserManager<ApplicationUser> UserManager
    ```
   </details>
6. Add this line in Program.cs - 47\
   app.UseAuthentication();
7. Note\
   এবার বিল্ড দিয়ে প্রজেক্ট চেক করে নিয়ে মাইগ্রেশন জেনারেট করতে পারি যদি মাইগ্রেশন জেনারেট না হয় তাহলে আইডেন্টিটির জন্য যে ডিফল্ট একটা মাইগ্রেশন তৈরি হয় জিরো জিরো জিরো দিয়ে এগুলো 
   ডিলিট করতে হবে তারপরে মাইগ্রেশন genite করে চেক করতে হলে যে আমাদের এনটিটি টেবিল এবং আইডেন্টিটি টেবিল দুইটাই তৈরি হয়েছে কিনা এখানে প্রবলেম হতে পারে শুধু আইডিটি টেবিল তৈরি 
   হতে পারে কিন্তু আমাদের যে এন্টিটি আছে সেটা তুই নাও হতে পারে সে ক্ষেত্রে নিচের এই কোডটুকু দিয়ে দিতে হবে ai package doita lagbe

   <details>
    <summary>ApplicationDbContext</summary>
    
    ```c#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(UserSeed.Users);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            // Add other entity configurations if needed

        }
    ```
   </details>
8. Add Scaffold Item -89\
   web project >> add >>New Scaffolded Item
  then select login and register page and select dbcontext then create
  এখানে যদি কোন এরর শুরু করে যে প্রজেক্ট লোডফিল্ড বা কনফিগারেশন ফিল্ড তাহলে আমাদের কোর্ট জেনারেশন ডিজাইন এবং কোড জেনারেশন অন util  এই দুইটা প্যাকেজ ম্যানেজার আপডেট করতে 
  হবে\
  Microsoft.AspNetCore.Identity.UI\
  Microsoft.EntityFrameworkCore.Design



> Summery:  আমাদের অ্যাপ্লিকেশন ডিবি কনটেক্সট ক্লাসে যে ইনভাইট করা হয়েছে ডিবি কোনটেক্স থেকে সেটা চেঞ্জ করে আইডেন্টিটি ডিবি কন্টেক্স সেট করে দিতে হবে.  তারপর persistence এর ভিতরে মেম্বারশিপ এর জন্য কিছু ক্লাস আছে যেগুলো কপি পেস্ট করে দিয়ে দিব.
তারপর আমরা যে Addidentity  এর জন্য যে সার্ভিস কালেকশন এক্সটেনশন তৈরি করেছি তার ভিতরে কিছু কনফিগারেশন দিয়ে দিতে হবে আগেরগুলো রিমুভ করে যার মধ্যে আমাদের তৈরি করা অ্যাপ্লিকেশন রোল ইউজার ইউজার ম্যানেজার এগুলো থাকবে লগইন, লগ আউট কুকীএগুলোর পাত থাকবেএবং পাসওয়ার্ড এর কিছু রিকোয়ারমেন্ট থাকবে এবং একাউন্ট লগের কিছু রিকোয়ারমেন্ট থাকবে,
তারপরে লগইন পার্শিয়াল যে ফাইল টা আছে সেই ফাইলে কিছু মডিফাই করে দিতে হবে  আইডেন্টিটি ইউজারের পরিবর্তে আমাদের তৈরি করা অ্যাপ্লিকেশন ইউজার দিয়ে দিতে হবে. এবার আমাদের অ্যাপ্লিকেশন ডিবি কনটেক্স এ আইডেন্টিটি ডিবি context ভিতরে কিছু প্যারামেটার দিয়ে দিতে হবে এবং প্রোগ্রাম ডট সিএসের ভিতরে গিয়ে অ্যাপ ডট ইউজ অথেন্টিকেশন এটা দিয়ে দিতে হবে এবার আমরা যদি ওয়েব প্রজেক্ট থেকে এড স্কাবলড আইটেম দিয়ে লগইন এবং রেজিস্টার পেজ ক্রিয়েট করতে পারি তবে সে ক্ষেত্রে এগুলো রেজার পেজ হবে এই জন্য আমাদেরকে সার্ভিস কালেকশনের নিচে addrazorpage দিয়ে দিতে হবে
স্কাবলড আইটেম তৈরি করতে গেলে আমাদের কোন জেনারেটর ডট utils এবং কোড জেনারেটর ডট design এই দুইটা প্যাকেজ আপডেট করে রাখতে হবে 

   <details>
    <summary>Dummy</summary>
    
    ```c#
    
    ```
   </details>

   
## Class-31 Register Page Convert
1. Create Account Controller\
   কন্ট্রোলারের ভিতরে গেট এবং পোস্ট মেথডের জন্য IAcctionResult  থাকবে এবং কিছু ডিপেন্ডেন্সি ইনজেকশন থাকে
   <details>
    <summary>Account Controller(26) and add dependency(33,36)</summary>
    
    ```c#
    namespace Crud.web.Controllers
    {
    public class AccountController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IEmailSender _emailSender;
        public AccountController(ILifetimeScope scope, ILogger<AccountController> logger, 
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;

        }
         public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            model.ReturnUrl??= Url.Content("~/");
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = model.Email, 
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                    pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href  ='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
           }
        
           }
        }
    ```
   </details>
2. Create Register Model Class\
   এই মডেলের ভিতরে আমরা রাজর যে ভিউটা আছে ভিউ থেকে from ভ্যালুগুলো নিয়ে আসবো, এই মডেলের ভিতরে আমরা রাজর যে ভিউটা আছে ভিউ থেকে from ভ্যালুগুলো নিয়ে আসবো,
   এবং নিচে যে রিটার্ন ইউআরএল আছে সেটার nullable করে দিব **(67)**  public string? ReturnUrl { get; set; }
   যদি আমরা আরো কোন ফিল্ড প্রয়োজন মনে করি তাহলে এখানে অ্যাড করতে পারব যেমন আমরা এড করেছি ফাস্ট নেম এবং লাস্ট নেম 

   <details>
    <summary>Create Register Model Class(27)</summary>
    
    ```c#
    namespace Crud.web.Models
    {
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }

        public string? ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
      }
    }
    ```
   </details>
   <details>
    <summary>RegisterModule Binding to WebModule For DI(34)</summary>
    
    ```c#
    public class WebModule : Module
    {      
        
        protected override void Load(ContainerBuilder builder)
        { 
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
    ```
   </details>
3. Disable External Authentication-37
4. Add Extra input field in ResponseModel-45
5. Create View Page-51\
   ভিউপেজ ক্রিয়েট করে রাজর ভিউ পেজ থেকে সবকিছু কপি করে নিয়ে আসতে হবে তারপরে উপরে যে আমরা রেজিস্টার মডেল তৈরি করেছি সেটা মডেলের মধ্যে দেখিয়ে দিতে হবে এবং আগে যে পেজ      লেখা ছিল সেটার রিমুভ করে দিতে হবে এবং আমরা যদি এক্সট্রা কোন ফিল্ড এড করে থাকি মডেলের ভিতরে সেগুলোও এখানে নিচে অ্যাড করে দিতে হবে অবশ্য ভিউ পেস্টটা হবে রুট view মধ্যে কোন 
   এরিয়ার মধ্যে নয়,  কারণ আমরা root থেকেই লগইন করাবো\
   এবং form উপরে  এরিয়া,  কন্ট্রোলার,  একশন এইগুলো বসায় দিব **(55)**
   <details>
    <summary>View Page</summary>
    
    ```HTML
    @model RegisterModel
      @{
    ViewData["Title"] = "Register";
      }

   <h1>@ViewData["Title"]</h1>

   <div class="row">
    <div class="col-md-4">
        <form id="registerForm" asp-action="register" asp-controller="account" asp-area="" asp-asp-antiforgery="true" asp-route-returnUrl="@Model.ReturnUrl" method="post">
            <h2>Create a new account.</h2>
            <hr />
            <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
            <div class="form-floating mb-3">
                <input asp-for="Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                <label asp-for="Email">Email</label>
                <span asp-validation-for="Email" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="Password" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label asp-for="Password">Password</label>
                <span asp-validation-for="Password" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="ConfirmPassword" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label asp-for="ConfirmPassword">Confirm Password</label>
                <span asp-validation-for="ConfirmPassword" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="FirstName" class="form-control" autocomplete="username" aria-required="true" placeholder="Shamim" />
                <label asp-for="FirstName">First Name</label>
                <span asp-validation-for="FirstName" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="LastName" class="form-control" autocomplete="username" aria-required="true" placeholder="Hosen" />
                <label asp-for="LastName">Last Name</label>
                <span asp-validation-for="LastName" class="text-danger"></span>
            </div>
            <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
        </form>
       </div>

      </div>

      @section Scripts {
       <partial name="_ValidationScriptsPartial" />
      }
    ```
   </details>
> আমরা একটা কন্ট্রোল তৈরি করব এবং একটা মডেল ক্লাস তৈরি করব,  মডেল ক্লাসে rezor page  ভিতর থেকে ফর্মটা নিয়ে আসবো এবং সেখানকার গেস্ট এবং পোস্ট মেথডগুলো কন্ট্রোলারে নিয়ে আসবো 
 এবং সেগুলোর error  রিমুভ করব সে ক্ষেত্রে আমাদের কিছু ডিপেন্ডেন্সির প্রয়োজন হবে এবং সে ডিপেন্ডেন্সি টা দিয়ে error রিমুভ করে আমরা ওয়েব মিডিয়েলের সঙ্গে Dependency injection করে দিব 
 তারপরে একটা ভিউ পেজ ক্রিয়েট করব যেটা মডেল থেকে আসবে এবং ভিউ পেজের কনটেন্ট আসবে রেজোড়ের ভিউ পেজ থেকে তারপর সেখানে এরিয়া কন্ট্রোলার অ্যাকশন এগুলো দেখিয়ে দেব form ar 
 vitor এবং মডেল ক্লাসের ভিতরে আমাদের রিটার্ন ইউআরএল টা nullable এবং সেট করে দিতে হবে এবং কন্ট্রোলারে একটা ভিউ মডেল রিটার্ন করবে আই যেটা IAcction return করবে, ইমেল স্যান্ডার 
 এবং এক্সটার্নাল লগইন এগুলো ডিজাবুল করে দিব

## Class-32 Login-Logout Page
1. Create LoginModel-3\
    Copy inpitmethon property from login.cshtml.cs mvvm page and past Loginmodel also copy-past returnurl
    <details>
     <summary>LoginModel</summary>
    
     ```c#
     namespace Crud.web.Models
     {
     public class LoginModel
     {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
     }
     }   
     ```
    </details>
2. Register LoginModel into WebModule - 5
   <details>
    <summary>Dummy</summary>
    
    ```c#
    protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserUpdateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginModel>().AsSelf().InstancePerLifetimeScope();
        }
    ```
    </details>

3. Add Login IAction in AccountController-7\
    Copy Get and post method from login.cshtml.cs and past to AccountController
    <details>
     <summary>Add IAcction into AccountController</summary>
    
     ```c#
    //Login Page
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var model = _scope.Resolve<LoginModel>();
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(model.ReturnUrl);
                }
                
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); 
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
     ```
    </details>

4. Login View Page-14\
    copy login razor page login MVC page and remove error 
   <details>
     <summary>Login MVC Page</summary>
    
     ```c#
    @model LoginModel

    @{
    ViewData["Title"] = "Log in";
    }

    <h1>@ViewData["Title"]</h1>
    <div class="row">
    <div class="col-md-4">
        <section>
            <form id="account" method="post" asp-antiforgery="true" asp-controller="Account" asp-action="Login">
                <h2>Use a local account to log in.</h2>
                <hr />
                <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
                <div class="form-floating mb-3">
                    <input asp-for="Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                    <label asp-for="Email" class="form-label">Email</label>
                    <span asp-validation-for="Email" class="text-danger"></span>
                </div>
                <div class="form-floating mb-3">
                    <input asp-for="Password" class="form-control" autocomplete="current-password" aria-required="true" placeholder="password" />
                    <label asp-for="Password" class="form-label">Password</label>
                    <span asp-validation-for="Password" class="text-danger"></span>
                </div>
                <div class="checkbox mb-3">
                    <label asp-for="RememberMe" class="form-label">
                        <input class="form-check-input" asp-for="RememberMe" />
                        @Html.DisplayNameFor(m => m.RememberMe)
                    </label>
                </div>
                <div>
                    <button id="login-submit" type="submit" class="w-100 btn btn-lg btn-primary">Log in</button>
                </div>
                <div>
                    <p>
                        <a id="forgot-password" asp-area="" asp-controller="Account" asp-action="ForgotPassword">Forgot your password?</a>
                    </p>
                    <p>
                        <a asp-area="" asp-controller="Account" asp-action="Register" asp-route-returnUrl="@Model.ReturnUrl">Register as a new user</a>
                    </p>
                    <p>
                        <a id="resend-confirmation" asp-area="" asp-controller="Account" asp-action="ResendEmailConfirmation">Resend email confirmation</a>
                    </p>
                </div>
            </form>
        </section>
        </div>

    </div>

        @section Scripts {
     <partial name="_ValidationScriptsPartial" />
        }
     ```
    </details>


5. Edit LoginPartial-27\
    asp route add (area, controller, action)
   <details>
     <summary>LoginPartial Add</summary>
    
     ```c#
    @using Crud.Persistance.Features.Membership;
    @using Microsoft.AspNetCore.Identity
    @inject SignInManager<ApplicationUser> SignInManager
    @inject UserManager<ApplicationUser> UserManager

    <ul class="navbar-nav">
    @if (SignInManager.IsSignedIn(User))
    {
    <li class="nav-item">
        <a  class="nav-link text-dark"  asp-area ="Admin" asp-controller="Dashboard" asp-action="Index"  title="Manage">Hello @User.Identity?.Name!</a>
    </li>
    <li class="nav-item">
        <form  class="form-inline" asp-antiforgery="true" asp-area=" " asp-controller="Account" asp-action="Logout" asp-route-returnUrl="@Url.Action("Index", "Home", new { area = "" })">
            <button  type="submit" class="nav-link btn btn-link text-dark">Logout</button>
        </form>
    </li>
    }
    else
    {
    <li class="nav-item">
            <a class="nav-link text-dark" asp-area=" " asp-controller="Account" asp-action="Register">Register</a>
    </li>
    <li class="nav-item">
            <a class="nav-link text-dark" asp-area=" " asp-controller="Account" asp-action="Login">Login</a>
    </li>
    }
    </ul>
     ```
    </details>

6. Logout Scaffold and IAction-35\
    Create a New Scaffold item Logout and post IAction past to Account controller
   <details>
     <summary>Logout IAction into AccountController</summary>
    
     ```c#
    public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
     ```
    </details>
7. Restriction Apply in Dashboard-60\
    Admin area could not access without login, if we add ** Authorize ** anotation in top of the 
    sepecific controller then we can't access without login all of actioe under the controller.
    But ws need access specific action without action need ** Anonymouse ** anotation top of the action
   <details>
     <summary>Area Access Restriction</summary>
    
     ```c#
     [Area("Admin"), Authorize] //used for restriction
     [AllowAnonymous]//allow without login
     ```
    </details>
  Note: Authontication: Check userd login or not
        Authorization: Check user permited the visit an area
1. Title here
   <details>
     <summary>Dummy</summary>
    
     ```c#
    
     ```
    </details>
   