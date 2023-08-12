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
  

4. Modify ServiceCollectionExtention> AddIdentity-37\
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
5. Edit _LoginPartial-43\
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

## Class-33 (Role-Management)
1. SettingController Create(10,21,29)\
    এখানে লগার, স্কপ এবং রোল তৈরী এবং রোল এসাইন এর একশন তৈরী করা হই
   <details>
     <summary>SettingController-29</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Controllers
    {
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<SettingController> _logger;
        public SettingController(ILogger<SettingController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Roles()
        {
            return View();
        }

        public async Task <IActionResult> CreateRole()
        {
            var model = _scope.Resolve<RoleCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
               await model.CreateRole();
            }
           return RedirectToAction(nameof(Roles));
        }
    }
    }

     ```
    </details>
2. View Page Create & CreateRole Action View-25\
    রোল এর ভিউ এবং ক্রিয়েট রোল এর ভিউ থাকবে। 
   <details>
     <summary>CreateRole view</summary>
    
     ```c#
    @model RoleCreateModel
    @{
    ViewData["Title"] = "CreateRole";
    }

    <div class="container-fluid">
    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Create User</h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <form role="form" asp-antiforgery="true" asp-action="CreateRole"
                      asp-area="Admin" asp-controller="Setting" method="post">
                    <div class="card-body">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <div class="form-group">
                            <label asp-for="Name"></label>
                            <input type="text" class="form-control" asp-for="Name" placeholder="Enter title">
                            <span asp-validation-for="Name" class="text-danger"></span>
                        </div>
                        
                    </div>
                    <!-- /.card-body -->

                    <div class="card-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
            <!-- /.card -->

        </div>
        <!--/.col (left) -->
     </div>
     <!-- /.row -->
     </div><!-- /.container-fluid -->
     @section Scripts
    {
    <partial name="_ValidationScriptsPartial" />
     }



     ```
    </details>
3. CreateRoleModel-14\
    এখানে রোল তৈরি কড়ার জন্যে Name একটা প্রপার্টি এবং Rolemanager & usermanager field
    থাকবে। 
   <details>
     <summary>CreateRoleModel-19</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Models
    {
    public class CreateRoleModel
    {
        [Required]
        public string Name { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public CreateRoleModel() { 

        }
        public CreateRoleModel(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        public async Task  CreateRole()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                await _roleManager.CreateAsync(new ApplicationRole(Name));
            }
        }
     }
     }

     ```
    </details>
4. RoleListModel-14
   <details>
     <summary>Dummy</summary>
    
     ```c#
    
     ```
    </details>
5. Binging WebModule-14
   <details>
     <summary>WebModule</summary>
    
     ```c#
    builder.RegisterType<CreateRoleModel>().AsSelf().InstancePerLifetimeScope();
    builder.RegisterType<RoleListModel>().AsSelf().InstancePerLifetimeScope();
     ```
    </details>
6. Assign Role Controller-37\
    AssignRole আই- একশন বানাতে হবে। এখানে ডাটা তুলে আনার জন্নে Loaddata() method থাকবে।
   <details>
     <summary>AssignRole Action</summary>
    
     ```c#
    //asign role
        public async Task<IActionResult> AssignRole()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.LoadData();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.AssignRole();
            }
            return RedirectToAction(nameof(Roles));
        }
     ```
    </details>
7. RoleAssignModel class-39\
    এখানে list এর মধ্যে role and user তুলে নিয়ে আসবে। Loaddata used করে
    username অনুযায়ী রোল সেট কড়ার জন্য ২ টা প্রপার্টি নিতে হবে। 
   <details>
     <summary>RoleAssignModel</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Models
    {
    public class RoleAssignModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<SelectListItem>? Roles { get;private set; }
        public List<SelectListItem>? LUsers { get; private set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public RoleAssignModel() { 

        }
        public RoleAssignModel(RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }
        //used for Load Username and role name in vied page
        internal async Task LoadData()
        {
            LUsers = await (from c in _userManager.Users
                           select new SelectListItem($"{c.FirstName} {c.LastName}", c.UserName))
            .ToListAsync();

            Roles = await (from c in _roleManager.Roles
                           select new SelectListItem(c.Name, c.Name))
                     .ToListAsync();
        }
        //assign role
        internal async Task AssignRole()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(Username);
            await _userManager.AddToRoleAsync(user, RoleName);   
        }

    }
    }
     ```
    </details>
8. AssignRole View-50-14
   <details>
     <summary>AssignRole View </summary>
    
     ```c#
    @model RoleAssignModel
    @{
    ViewData["Title"] = "AssignRole";
    }

    <div class="container-fluid">
    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Create User</h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <form role="form" asp-antiforgery="true" asp-action="AssignRole"
                      asp-area="Admin" asp-controller="Setting" method="post">
                    <div class="card-body">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <div class="form-group">
                            <label asp-for="Username"></label>
                            <select asp-items="@Model.LUsers"  asp-for="Username" ></select>
                            <span asp-validation-for="Username" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <label asp-for="RoleName"></label>
                            <select asp-items="@Model.Roles" asp-for="RoleName"></select>
                            <span asp-validation-for="RoleName" class="text-danger"></span>
                        </div>

                    </div>
                    <!-- /.card-body -->

                    <div class="card-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
            <!-- /.card -->

        </div>
        <!--/.col (left) -->
    </div>
    <!-- /.row -->
    </div><!-- /.container-fluid -->
    @section Scripts
    {
    <partial name="_ValidationScriptsPartial" />
    }

     ```
    </details>
9. Register AssignRole -58
   <details>
     <summary>WebModule</summary>
    
     ```c#
    builder.RegisterType<RoleAssignModel>().AsSelf().InstancePerLifetimeScope();
     ```
    </details>
10. Policy Based Authorization-80\
    একাধিক Role কে একত্র করে একটা পলেছি বেজ authorization তৈরি করা হয়
     পলেছি বেজ এ যে কইটা role অ্যাড করব , কোন ইউজার আআর ওপর  সেই পলেছি অ্যাপ্লাই
     করতে হলে আবসসই পলেছি সব রোলে ইউজার এর মধ্যে অ্যাড করতে হবে। 
     একটা area or controller a  একাধিক পলিচ্য অ্যাড করা যাই  না।
    <details>
     <summary>Code in servicecollectionextention</summary>
    
     ```c#
    //Policy Based Role Management
    services.AddAuthorization(options =>
    {
        options.AddPolicy("ITPerson", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("HR");
            policy.RequireRole("IT");
         });
    });
     ```
    </details>

11. Configure Claim Based-90/
    <details>
    <summary> add claim option in ServiceCollectionExtention</summary>
    
     ```c#
    //Role Management
    services.AddAuthorization(options =>
    {
        //Policy Based
        options.AddPolicy("ITPerson", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("HR");
            policy.RequireRole("IT");
        });
        //Claim Based
        options.AddPolicy("UserViewPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("ViewUser", "true");
        });
    });
     ```
    </details>
    <details>
     <summary>Set authorization role for view</summary>
    
     ```c#
    [Authorize(Policy = "UserViewPolicy")]
     ```
    </details>
12. Create AssignClaim IAction -94
    <details>
     <summary>AssignClaim</summary>
    
     ```c#
    //Assign Claim
    public async Task<IActionResult> AssignClaim()
    {
        var model = _scope.Resolve<RoleAssignModel>();
        await model.AsignStaticClaim();
        return View();
    }
     ```
    </details>
    <details>
     <summary>create AsignStaticClaim() in RoleAssignModel</summary>
    
     ```c#
    internal async Task AsignStaticClaim()
    {
        ApplicationUser user = await _userManager.FindByNameAsync("it@crud.com");
        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ViewUser", "true"));
    }
     ```
    </details>
    now run project and go to AssignClaim action url. তাহলেই ক্লেয়াম অ্যাড হয়ে যাবে নিদ্দিস্ত ইউজার এ 
    claim  ইউজার এবং রোল দুইতাতে দেওয়া যায়। 
13. Alternative option for Claim Based - 104
    <details>
     <summary>add claim option in ServiceCollectionExtention</summary>
    
     ```c#
    //Alternative option for Claim Based
    options.AddPolicy("UserViewRequirementPolicy", policy =>
         {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new UserViewRequirement());
        });
    });
        //part of Alternative option for Claim Based
    services.AddSingleton<IAuthorizationHandler, UserViewRequirementHandler>();
     ```
    </details>
    <details>
     <summary>Add UserViewRequirement method in infrastructure </summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    { 
    public class UserViewRequirement : IAuthorizationRequirement
    {
    }
    }
     ```
    </details>
    <details>
     <summary>Add UserViewRequirementHandler method in infrastructure</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public class UserViewRequirementHandler :
          AuthorizationHandler<UserViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               UserViewRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewUser" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    }

     ```
    </details>
    Note: nameof(method name)-31 if we pass method name as a string try to used nameof 

## Class-34 (Web API)
1. Service-02\
    যখন আমরা কোন সফটওয়্যার থেকে সেবা গ্রহন করি সেটা হছে service like pament gateway service.
    service আবার ২ ব্যবহার করা হয় ১) open(any-one can used) 2) restricted (need to credential)
    Service 2 ধরনের হয় 1) web service 2) windows service/Nativ service
2. Web Service-04\
    Web service ডাটা provide  করার জন্য একটা web application, এটা Presentation , View  Provide করে না। 
    Web service  ওয়েব সারভারে রাখতে হয়। 
3. WEB Api-13\
    Application Programming/Programable Interface. web api হতে হলে একটা service এর অবশ্যই restricted হতে হবে।  
4. Create Dot.net Core Web Api Project-25\
    একটা ওয়েব API প্রোজেক্ট নিতে হবে। প্রোজেক্ট তৈরি করতে গেলে আমরা দেখতে পারব Aditional Information এর মধ্যে minimal Api 
    সিলেক্ট করার একটা অপশন আছে।
    minimal apl হছে কন্ট্রোলার ছাড়া প্রোজেক্ট তৈরি করা। কারন controller  একটা হেভি class । প্রোজেক্ট এর লোড কমানোর জন্য এইটা ব্যবহার করা হয়
    Monolethik Application এ minimal api use  করে তেমন উপকার হয় না microservice application a minimal api ব্যবহার করা ভাল।
5. RSET Api-33\
    Rest api মূলত একটা convention. Controller, Action এর নাম একটা নিদিষ্ট ফরম্যাটে করার কনভেনশন হছে রেস্ট আপিয়াই। 
    web project এর controller এ অ্যাকশান গুলোর রিটার্ন টাইপ IAction না হয়ে IEnamurable হয় কারন web- api এর ভিউ থাকে না। 
6.  Serilog  & Autofac Configure\
    SerilogAsp.Net Core & Autofac প্যাকেজ ইন্সটল করে program.cs  configure korte hobe
    <details>
     <summary>Serilog configure in Program.cs-71</summary>
    
     ```c#
    builder.Host.UseSerilog((hc, lc) => lc //hc== hosting context lc= loging context
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
     );
     ```
    </details>
    <details>
     <summary>Serilog configure in Apsetting.jeson</summary>
    
     ```c#
    "Serilog": {
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/web-log-.log",
          "rollingInterval": "Day"
        }
      }
     ]
     },
     ```
    </details>
    <details>
     <summary>Autofac configure in Program.cs-77</summary>
    
     ```c#
        // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //collect MigrationAssembly Path
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    //Autofac configuration Start
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new PersistanceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new ApiModule());

    });
    //Autofac configuration End
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    //Auto Mapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     ```
    </details>

7. Create a controller-50\
    একটা controller create হবে এবং logger & scop এর dependency injection করতে হবে। 
    তারপর Get, Post,Put, Delete এই মেথড গুলো তৈরি করতে হবে। এগুলোর কাজ হবে CRUD Operation করা। 
    controller এর route এ একটা ভার্সন নাম্বার দিতে হয়, কারন ভার্সন মেইন্তেইন করেই api url তৈরি করতে হয়। 

   <details>
     <summary>UserController</summary>
    
     ```c#
    namespace Crud.API.Controllers
    {
    [Route("v3/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILifetimeScope scope, ILogger<UsersController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get() { 
            try
            {
                var model = _scope.Resolve<UserModel>();
                return model.GetUsers();
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Couldn't get courses");
                return null;
            }
        }
        [HttpGet("{id}")]
        public User Get(Guid id) {
            var model = _scope.Resolve<UserModel>();
            return model.GetUser(id);
        }
        //[HttpGet("{name}")]
        //public User Get(string name)
        //{
        //    var model = _scope.Resolve<UserModel>();
        //    return model.GetUser(name);
        //}

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            try {
                model.ResolveDepenency(_scope);
                model.CreateUser();
                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Put(UserModel model)
        {
            try
            {
                model.ResolveDepenency(_scope);
                model.UpdateUser();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<UserModel>();
                model.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete user");
                return BadRequest();
            }
        }
        }
    }
     ```
    </details>

8. Create a Model Class-53/
    controller এ মেথড গুলা লেখা হইসে সেগুলার ইমপ্লেমেন্ত করতে হবে , dependency resolve করতে হবে। মডেল ক্লাস এর একটা 
    এমটি constractor থাকবে। এবং একটা paramiterized constractor থাকবে যেখানে পারামিতের হিসাবে থাকবে IService

    <details>
     <summary>UserMode Class3</summary>
    
     ```c#
    namespace Crud.API.Models
    {
    public class UserModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public UserModel()
        {

        }
        public UserModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public void ResolveDepenency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<User>? GetUsers()
        {
            return _userService?.GetAllUser();
        }
        internal void DeleteUser(Guid id)
        {
            _userService?.DeleteUser(id);
        }
        internal void CreateUser()
        {   
            _userService?.CreateUser(Name, Email, Phone, Address);
        }
        internal void UpdateUser()
        { 
            _userService?.UpdateUser(Id, Name, Email, Phone, Address);
        }
        //internal User GetUser(string name)
        //{
        //    return _userService.GetUser(name);
        //}
        internal User? GetUser(Guid id)
        {
            return _userService?.GetUser(id);
        }

        public async Task<object?> GetPagedUsers(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _userService? .GetPagedUserAsync(
               dataTablesUtility.PageIndex,
               dataTablesUtility.PageSize,
               dataTablesUtility.SearchText,
               dataTablesUtility.GetSortText(new string[] { "Id", "Name", "Email", "Phone", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email,
                                record.Phone,
                                record.Address,
                                record.Id.ToString(),

                        }
                    ).ToArray()
            };
        } 
     }
     }
     ```
    </details>

9. Create ApiModule class-70\
    model class বাইন্ডিং করার জন্য একটা Module class তৈরি করতে হয় এবং এই ক্লাস টা program.cs a অটো ফেক এর ভিতর বাইন্ডিং করতে হবে।  
    <details>
     <summary>ApiModule</summary>
    
     ```c#
    namespace Crud.API.Models
    {
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            base.Load(builder);
        }
     }
     }
     ```
    </details>
## Class-35 (Web Api-JWT Token)
1. JWT Token-55\
    ইউজার যখন কোন রিকুয়েস্ট পাঠাবে সারভারএ তখন ইউজার কে লগিন করতে হয়, এখন api তে যখন ইউজার রিকুয়েস্ট পাঠাবে
    তখন api HTTP based হওয়াই ইউজার এর ক্রিদেন্তিয়াল ধরে রাখবে না। এই সমসসা সমাধানে জন্য JWT Token ব্যবহার করা হয়।
    এটা একটা টোকেন তৈরি করে প্রতি রিকুয়েস্ট এর সাথে পাঠাই দেই যার কারনে বারবার লগিন করতে হয়ই না।
    সহজ কথাই JWT হছে API request authentication system
2. Create TokenController-59\
    এখানে ইউজার ম্যানেজার, সাইন-ইন ম্যানেজার, আই-কনফিগারাসিওন, টোকেন সার্ভিস থাকে।
    Get method এর মাধমে email, password নিয়ে চেকিং করে, সব ঠিক থাকলে টোকেন সার্ভিস
    এর মাধমে টোকেন জেনেরাট করে স্ট্রিং করে রিটার্ন করে দেই। 

    <details>
     <summary>TokenController</summary>
    
     ```c#
    namespace Crud.API.Controllers
    {
    [Route("v3/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase 
    {  
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email, string password)
        {
            if (email != null && password != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

                if (result != null && result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _tokenService.GetJwtToken(claims);

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
    }
     ```
    </details>
3. Create TokenService Class for create Token-60\
    এখানে ইন্টারফেস এবং ক্লাস ২ টাই থাকবে। এইটা infrastructure layer ar security folder এর ভিতর তৈরি করব। 
    এর কাজ হবে টোকেন জেনারেত করা । এবং আপ সেটিং এ key টা দিয়ে দিতে হবে। 
    need package: Microsoft.AspNetCore.Authentication.JwtBearer
    <details>
     <summary>ITokenService</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
    }
     ```
    </details>
    <details>
     <summary>TokenService</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration config)
        {
            _configuration = config;
        }
        //Used to generate token
        public async Task<string> GetJwtToken(IList<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
    }
     ```
    </details>
    <details>
     <summary>Appsetting.jeson-67 for token key</summary>
    
     ```c#
    "Jwt": {
    "Key": "akjfjskghghjkfskjwjfewjifjksdjfksjfkdsfk",
    "Issuer": "https://localhost:44322",
    "Audience": "https://localhost:44322",
    "ClientId": "Demo",
    "Subject": "AccessToken"
    },
     ```
    </details>
4. Binding Tokenservice in module class-91
    <details>
     <summary>Tokenservice Binding for dependency</summary>
    
     ```c#
    public class InfrastructureModule : Module
    {     
        public InfrastructureModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

        }
        
    }
     ```
    </details>
5. Add AddIdentity in Program.cs-92\
    Need Package: Microsoft.AspNetCore.Identity.UI
    <details>
     <summary>AddIdentity add in program file</summary>
    
     ```c#
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    //Auto Mapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddIdentity();
     ```
    </details>


## Class-36 (Generate Migration in Console App)
1. Web Service (8)\
    Machine to Machine communicate করে এজন্য ইউজার ইন্টারফেস থাকে না।
    বেশিরভাগ সময়ই web service কে অন্য একটি প্রগ্রাম ব্যবহার করে।
    Web service ডাটা provide  করার জন্য একটা web application, এটা Presentation , View  Provide করে না। 
    Web service  ওয়েব সারভারে রাখতে হয়। 
2. Web Application\
    Web Application has User Interface for Humen interaction. 
    it is dynamic application.(Database driven, business logic, program). 
    it is subset of web site.
3. Web Site\
    it is a static site. It created by html, css only. like portfolio site
    it is a superset of web application.

    Note: Every web applition can be web site but web site can not be web applition.
4. WEB Api-13\
    Application Programming/Programable Interface. web api হতে হলে একটা service এর অবশ্যই restricted হতে হবে।  
    it follow the RSET Atchitucture (GET, POST, PUT, DELETE)
5. Console Project for Migration Generator-15\
    এখানে Logger, Dependency Injection এর কনফিগারেশন থাকবে। 
    Need this package: Autofac.Extensions.DependencyInjection
    Microsoft.EntityFrameworkCore.Design
    Serilog.Extensions.Hosting
    Serilog.Settings.Configuration
    এবং সব প্রোজেক্ট এর reference দিয়ে দিতে হবে। 
    
    <details>
     <summary>Program</summary>
    
     ```c#
    namespace Crud.MigrationRunner
    {
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            //collect appsetting.jeson file path
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory());
            string settingsPath = Path.Combine(root.Parent.Parent.Parent.FullName, "appsettings.json");
            //load appsetting
            _configuration = new ConfigurationBuilder().AddJsonFile(settingsPath, false) 
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _migrationAssemblyName = typeof(Program).Assembly.FullName;

            //Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            try {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally { 
                Log.CloseAndFlush();
            }
        }

        //Dependency Injection
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog()
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new PersistanceModule(_connectionString, _migrationAssemblyName));
                builder.RegisterModule(new ApplicationModule());
                builder.RegisterModule(new InfrastructureModule());
            });
    }
    }
     ```
    </details>
6. Add Authentication in API Project-50\
  
    Separate bellow code block from servicecollectionextention to program.cs file

    <details>
     <summary>Cookie based authuntication setting in Program.cs </summary>
    
     ```c#
    //cookie setting for identity for web
    builder.Services.AddAuthentication()
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
            options.LogoutPath = new PathString("/Account/Logout");
            options.Cookie.Name = "FirstDemoPortal.Identity";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        });
     ```
    </details>
    Add this bellow block in API Program.cs file then det the authintication base in IAction
    then goto postman and collect jwt token then goto a method which we add authenticatio
    like GET url and pest the Authorization  select type Bearer Token and pest the token
    in token field
    
    ![image](https://github.com/Shamim448/aspnet/assets/43339514/412adfb9-8290-4350-9ea3-9dbd609b302f)

    <details>
     <summary>Add Policy based Authintication in API Project-51 and class 37-03</summary>
    
     ```c#
    //Authentication service for Jwt token
    builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        });
    builder.Services.AddAuthorization( options =>
        {
            //Alternative option for Claim Based
            options.AddPolicy("UserViewRequirementPolicy", policy =>
            {
                policy.AuthenticationSchemes.Clear();
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new UserViewRequirement());
            });
    });
    //part of Alternative option for Claim Based
    builder.Services.AddSingleton<IAuthorizationHandler, UserViewRequirementHandler>();
     ```
    </details>
## Class-37 (Worker Service)

1. Worker Service(18)\
    Web service  কে আমরা সার্ভার এ host করে URL দিয়ে HTTP Method a access করি। 
    কিন্ত worker service এক্তা computer এ service হিসাবে run করে। এতাকে HTTP দিয়ে call করা যাই না। 
    এটা নিজে নিজে রান করতে থাকে, এটা দিয়ে কিছু কাজ করা যাই কিন্তু এতাকে কল করা যাই না। 
2. Create Worker Service Project(35)\
    Project create করে AutoFac, serilog configure করতে হবে Program.cs file এ । 
    appsetting.json file ta o add korte hobe.
    Package: Microsoft.Extensions.Hosting.WindowsServices
    Serilog.Extensions.Hosting
    Serilog.Settings.Configuration
    Serilog.Sinks.File
    <details>
     <summary>Program.cs - 67</summary>
    
     ```c#
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var migrationAssemblyName = typeof(Worker).Assembly.FullName;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            Log.Information("Application Starting up");

            IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new WorkerModule());
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                })
                .Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
    }
     ```
    </details>
    <details>
     <summary>WorkerModule</summary>
    
     ```c#
    namespace Crud.EmailWorker
    {
    internal class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
    }
     ```
    </details>
    <details>
     <summary>Appsettings - 77</summary>
    
     ```c#
    "Serilog": {
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "F:/Projects/aspnet/Practice/GroupStudy/Crud.EmailWorker/Logs/worker-log-.log",
          "rollingInterval": "Day"
        }
      }
     ]
     }
     ```
    </details>
3. Published worker service - 77\
    প্রোজেক্ট এ right button click  করে Folder select করে next তারপর location দিয়ে (if needed)
    Finished করতে হবে। এবার publish button এ click করতে হবে। 
    এবার service install করার জন্য command promt addministrator mode এ ওপেন করতে হবে
    sc.exe create service name binpath=service ar exe file path start=auto
    Uninstall service: stop service 
    sc.exe delete service name

    Note: appsetting ar log folder path pc এর drivar path দিতে হবে।


## Class -38 (Unit Test)
1. Type of Testing(18-26)\
    i) Unit Test
    ii) Intregration Test
    iii) System Test
    iv) Load Test
    আরও কিছু Test আছে, যা এই কোর্সে আলোচনা করা হবে না। যেমনঃ  
    (Smoke Test, End to end test, Functional Test etc) 

    i) Unit Test: যদি কোন Method এর জন্য Test লিখি তাহলে ঐ method এর ভিতর যদি অন্য কোন Class এর কোন Method থাকে 
    (Dependency হিসাবে) তাহলে সেই Method কে Call করা যাবে না। তবে যদি same class এর কোন Public/Private Method এর ওপর Dependent হয়
     তাহলে সেই Method কে Call করা যাবে। 
    ii) Intregration Test: যদি কোন Method এর Testing Code হতে অন্য কোন Class এর Medhod কে (Dependent ) হবার কারনে Call
    করা যায় তাহলে সেটা Intregration Test
    iii) System Test: আর যদি কোন Method হতে পুরা System (Database, Filesystem, Network) Call করা যায়, তবে সেটা হবে System Test
    iv)  Load Test: At a time কত জন ইউজার Software ব্যবহার করতে পারবে সেটা Laod Test দ্বারা করা হয়। 

    Note: বিভিন্ন ধরনের Testing এর Codeing গত কোন পার্থক্য নেই, শধুমাত্র Call কতদূর পর্যন্ত করা যাবে তার ওপর ভিত্তি করে Testing এর
    প্রকারভেদ নির্ণয়ই করা হয়।

2. Unit Testing এর বৈশিষ্ট্য -(26-27)\
   * এক Class এর Method হতে অন্য Class এর Method কে কল করা যাবে না।
     কারন যদি Test Failed হয়, তাহলে কোন Method এর কারনে Failed হইসে বুঝা 
     যাবে না। যেকোন একটা  Method Failed হলেই Test Failed হবে।
   * External Resource এ Call যেতে পারবে না।(Database, Filesystem, Network)
   * Test one Click এ Run হতে হবে।
   * Test যেকোনো Machine হতে Runable হতে হবে কোন প্রকার Configuration ছাড়াই ।
   * Test খুব দ্রত Run হতে হবে।
3. Create NUnits Test Project-41\
    একটা Test Folder এর ভিতর NUnit Test Project Create করব।
    Project এর Name Structure হবে, যেই Project এর জন্য Test Project Create করছি 
    সেই Name পুরোটা দিয়ে তারপর .Test (Crud.Application.Test)
    Need Package: Autofac.Extras.moq

4. এখানে Unit Test অনুশীলন করার জন্য Application Project এ কিছু Method Create করছি। 
    <details>
     <summary>AccountService</summary>
    
     ```c#
    namespace Crud.Application.Features.Training.Services
    {
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        public AccountService(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }
        public void CreateAccount(string username, string password)
        {
            if(username == null || password == null)
                throw new InvalidDataException();
             
            if(username.Length > 30)
                username = username.Substring(0, 30);
            //code to create account
            _accountRepository.CreateAccount(username, password);
            _emailService.SendAccountCreationEmail(username);
        }
    }
    }
     ```
    </details>
    <details>
     <summary>EmailService</summary>
    
     ```c#
    namespace Crud.Application.Features.Training.Services
    {
    public class EmailService : IEmailService
    {
        public void SendAccountCreationEmail(string email)
        {

        }
    }
    }
     ```
    </details>

    <details>
     <summary>IAccountRepository</summary>
    
     ```c#
    namespace Crud.Application.Features.Training.Repositories
    {
    public interface IAccountRepository
    {
        public void CreateAccount(string username, string password);
    }
    }
     ```
    </details>
5.  Unit Test Configure-69\
    Class Name টা হবে যেই Class এর জন্য Test এর Class Name ও same হতে হবে।
    যেগুলো Dependancy আছে সেগুলা MOCK field হিসাবে নিতে হবে। 
    এবং সেগুলা setup এর ভিতর Asign করে দিতে হবে।
    
    Test Method এর নামে করনের একটা ফরম্যাট আছে। 
    (CreateUser_LargeUsername_TruncateUsername)

    * SETUP: It's Run before every Test run
    * OneTimeSetup: Its run onetime when unit test run
    * Teardown : It's Run after every Test run
    * OneTimeTearDown: Its run onetime when unit test run finished


    <details>
     <summary>AccountServiceTests</summary>
    
     ```c#
    namespace Crud.Application.Tests
    {
    public class AccountServiceTests
    { 
        private AutoMock _mock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IEmailService> _emailServiceMock;
        private AccountService _accountService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }
        [SetUp]
        public void SetUp()
        {
            _accountRepositoryMock = _mock.Mock<IAccountRepository>();
            _emailServiceMock = _mock.Mock<IEmailService>();
            _accountService  = _mock.Create<AccountService>();
        }
        [TearDown]
        public void Teardown() 
        {
            _accountRepositoryMock.Reset();
            _emailServiceMock.Reset();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }
        [Test]
        public void CreateUser_LargeUsername_TruncateUsername()
        {
            //Arrange
            const string username = @"mynameisshamimhosenmynameisshamimhosen
                                    mynameisshamimhosenmynameisshamimhosen";
            string expectedResult = username.Substring(0, 30);
            const string password = "fgdhgfhgngdfjkgj";
            _accountRepositoryMock.Setup(x => x.CreateAccount(expectedResult, password)).Verifiable();
            //Act
            _accountService.CreateAccount(username, password);
            //Assert
            _accountRepositoryMock.VerifyAll();
        }
    }
    }
     ```
    </details>
## Class -39 (Unit Test)
1.  
    <details>
     <summary></summary>
    
     ```c#
    
     ```
    </details>
## Class -40 (Docker)

    ডকার এ কাজ করতে হলে প্রথমে একটা Dockerfile নিতে হবে যেই ফাইলের কোন এক্সটেনশন থাকবে না.
    ডকারের কমান্ড এবং Dockerfile  এর কমান্ড আলাদা. 
    * Docker Hub: ডকার ভাব হচ্ছে ডকার ইমেজ স্টোর করার একটা জায়গা. এখানে একটা অ্যাকাউন্ট ক্রিয়েট করে নিতে হয়
    

  ![image](https://github.com/Shamim448/aspnet/assets/43339514/1f9c8a08-0710-479f-a85e-ab2173e403f6)
  Docker Command\
  ![image](https://github.com/Shamim448/aspnet/assets/43339514/e0fb05da-e1cb-49db-b329-8d7d394cd1e0)

  #### Dockerfile Detailes-54\
  * Form: এই কমান্ড একটা বেজ ইমেজের নাম দিতে হয় যেমন আমরা যদি ডকারে গিয়ে ডকারের অফিসিয়াল ইমেজে যাই তাহলে  বিভিন্ন সিস্টেমের জন্য ইমেজ পাব  যা দরকার তৈরি করে রেখেছে . সেটাই হবে বেজ name.
  * Maintainer : যে এটা মেন্টেন করবে যেমন আমি নিজে 
  * Run shdo apt-get install apache2-y
  * এপাচি ইন্সটল করার সময় একটা প্রম আসে জিওগ্রাফিক লোকেশন ,তখন কমান্ড ফ্রন্ট হ্যাং হয়ে যায় এটা দূর করার জন্য নিচের কনফিগারেশন দিতে হবে 
  * ARG DEBIAN FRONTEND=noninteractive
    এই  এপাচি ইন্সটল করার পূর্বে এই কমান্ড দিলে তাহলে আর কমান্ডক্রমে জিওগ্রাফিক লোকেশন এর কথা জিজ্ঞেস করবে না
  * WORKDIR /var: workdir এর কাজ হচ্ছে আপনি ঢোকার ওপেন করার সাথে সাথে কোন ফোল্ডারে ঢুকতে চান সেটা 
  * EXPOSE 80: এই কমান্ড দ্বারা 80 port ওপেন করা হয় 
  * CMD apachect1 -D FOREGROUND: এই কোড দ্বারা মূলত এপাচি কে ফর রাউন্ডে নিয়ে আসা হয় অর্থাৎ এটা চালু অবস্থায় নিয়ে আসা হয় এটা না হলে ব্যাকগ্রাউন্ডে থাকবে এবং স্টপ হয়ে যেতে পারে 
    CMD কমান্ড মূলত প্রোগ্রাম এক্সিকিউশনের জন্য ব্যবহার করা হয় 

  62:  Docker Build -t ptoject name -f docker file path .
    ডকারে বিল্ড করার জন্য উপরের কমান্ডটা দিতে হবে এখানে মাইনাস টি -t এরপরে প্রজেক্ট nameদিতে হবে এবং মাইনাস এফ এরপরে আমার ডকা ফাইনের path টা দিতে হবে  তারপর ডট দিতে হবে তবে যদি আমরা কমান্ড promt যেখান থেকে রান করেছে সেখানেই যদি ঢোকার ফাইল থাকে তাহলে মাইনাস এফ এর পর path দেওয়ার প্রয়োজন নাই শুধু ডট দিয়েই কাজ চালিয়ে নিতে পারব 
  85: Docker Image Remove:- docker rmi -f imagename
  এখানে মাইনাস এফ মানে ফোর্টফুলি ডিলিট করবে আর কি. 

  107: Port change : docker run -p 8000:80 projectname

### Proces of Docker image installation\
1.  Create Dockerfile
2.  docker build -t image name -f Dockerfile path . (dot should be used in last)
3.  If any problem to face install Remove image : docker rmi -f imagename
4.  docker run -it imagename/ docker run
5.  apt-get (if need any application install)
6.  docker ps (Show running process)
7.  in need remove container: docker rm -f container id
8.  port change : docker run -p 8000:80 image name
## Class -41 (Dockerfile)
    ডকার ইউজ করার ক্ষেত্রে একটা ঝামেলা কর কাজ হচ্ছে ডাটাবেজ যেহেতু সাইটটা ডকার ইমেজে থাকবে এবং ডাটাবেজটা কোন সার্ভারে বা PC থাকবে তাদের মধ্যে কমিউনিকেশন করাটা একটা ঝামেলার কাজ 
    তবে এটা করা যাবে রিয়েল IP মাধ্যমে এজন্য আমাদের পিসির কিছু কমপ্লিফিকেশন ঠিক করে নিতে হবে .
    Mainten docker image run process we used (Docker compose) tools
1. Open SQL Server manager management in administrator mode 

2. Need some configuration from this link
    https://drive.google.com/file/d/1oGZF0SGXwUne9xyySlUTPsE3hv1M0i-u/view?usp=sharing

#### Create Docker File -69
    একটা প্রজেক্টের সব ফাইলের জন্য ঢকার ইমেজ তৈরি করা প্রয়োজন নেই শুধু সেই সমস্ত প্রজেক্ট এর জন্য ধোকার ইমেজ তৈরি করব যেগুলো রান করা যায় যেমন প্রজেক্ট এ পি আই প্রজেক্ট ওয়ার্কার সার্ভিস প্রোজেক্ট কোন প্রজেক্ট 
    অন্যান্য লাইব্রেরী প্রজেক্টগুলো সাপোর্ট ফাইল হিসাবে অ্যাড করে দিলেই হবে .
    যেই প্রজেক্টের জন্য ডকার ইমেজ তৈরি করব,  ডকার ফাইল তৈরি করতে হবে সেই প্রজেক্ট এর ভিতরেই .
#### DockerFile-72
* FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
    এখানে ফর্ম দ্বারা একটা Image/layer তৈরি করা হয়েছে যেটা sdk-7 এর উপরে তৈরি করা এবং যার নাম দিয়েছি build 
* WORKDIR /src
    ওয়ার্ক ডিয়ার  দ্বারা রুট ফোল্ডার src তৈরি করা হয়েছে
* RUN apt-get update && apt-get install -y nodejs
    এখানে প্রথমে আপডেট নেওয়া হয়েছে তারপরে nodejs ইন্সটল করা হয়েছে for Anguler 
* COPY ["FirstDemo.Web/*.csproj", "FirstDemo.Web/"]
    এর দ্বারা আমাদের কম্পিউটার থেকে আমার প্রজেক্ট এর csproj ফাইল কপি  কোনট্রেইনারে রাখবে, আমার কন্টেইনার চালানোর পর কপির কাজ করবে না আমরা যখন ইমেজ বানাবো তখনই কপি হয়ে যাবে 

* COPY ["FirstDemo.Infrastructure/*.csproj", "FirstDemo.Infrastructure/"]
    আগের মতই এখানেও আমাদের সাপোর্টিং যে লাইব্রেরী ফাইলগুলো আছে সেগুলো কপি হয় যাবে তবে মনে রাখতে হবে যে এখানে শুধু class লাইব্রেরী গুলো এড হবে যেসব প্রজেক্ট রান করা যায় সেসব প্রজেক্ট এড হবে না কারণ সেইসব প্রজেক্ট এর জন্য আলাদা আলাদা ইমেজ তৈরি করতে হবে 

* RUN dotnet restore "FirstDemo.Web/FirstDemo.Web.csproj"
    এই কমান্ড দ্বারা আমাদের যেসব প্যাকেজ(Nuget) লাগবে সেগুলো ইনস্টল হয়ে যাবে 
* COPY . .
    এই কপি দ্বারা আমাদের প্রজেক্ট ফোল্ডারের মধ্যে যা আছে সব কপি হয়ে যাবে ইটস এন্ড এভরিথিং লাইব্রেরী প্রজেক্ট , runnable প্রজেক্ট সব

* WORKDIR "/src/FirstDemo.Web"
    ডকারের এসআরসির ভিতরে আমার এই প্রজেক্টটি থাকবে 

* RUN dotnet build "FirstDemo.Web.csproj" -c Release -o /app
    প্রজেক্টটা বিল্ড হবে রিলিজ মোডে এবং সেটা অ্যাপ ফোল্ডারে গিয়ে থাকবে যেমন আমাদের স্টুডিও থেকে প্রজেক্ট দিলে বিল ফোল্ডার থাকে ঠিক এখানে অ্যাপ ফোল্ডারে থাকবে 

* FROM build AS publish
    বিল্ড কে base করে পাবলিশ নামে একটা Layer তৈরি করা হচ্ছে 
* RUN dotnet publish "FirstDemo.Web.csproj" -c Release -o /app
    এবং পাবলিশ layer রিলিজ মোডে  বিল্ড হবে এবং সেটাও অ্যাপ ফোল্ডারে তৈরি হবে এখানে উল্লেখ খোঁজে এখানকার অ্যাপ ফোল্ডার এবং Buila layer এর app ফোল্ডা ২ টা সম্পূর্ণ আলাদা . বিল্ড এবং পাবলিশ দুইটা আলাদা লেয়ার সুতরাং এদের ফোল্ডার গুলো আলাদা 

* FROM build AS final
    Build থেকে ফাইনাল নামে একটা Layerতৈরি করা হচ্ছে 
* WORKDIR /app
    সেটার রুট ডিরেক্টরী সেট করা হচ্ছে অ্যাপ 
* COPY --from=publish /app .
    Publish layer এর অ্যাপ থেকে ফাইল কপি করে ফাইনালের অ্যাপ folder রাখা হচ্ছে 

* ENTRYPOINT ["dotnet", "FirstDemo.Web.dll"]
    এন্ট্রি পয়েন্ট CMD মতোই একটা কমান্ড, এন্ট্রি পয়েন্ট কন্টেনার যখন চালু হয় তখন এক্সিকিউট হয় এই কমান্ডের পরে আর কোন ধরনের কমান্ড দেওয়া যাবে না
     এর প্রথম প্যারামিটার হচ্ছে কোন tools কাজ করবে সেটা যেমন ডটনেট এবং দ্বিতীয় প্যারামিটার হচ্ছে কোন DDL রান করবে সেটা 





    <details>
     <summary></summary>
    
     ```c#
    
     ```
    </details>
    <details>
     <summary></summary>
    
     ```c#
    
     ```
    </details>
    <details>
     <summary></summary>
    
     ```c#
    
     ```
    </details>






