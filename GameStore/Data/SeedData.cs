using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed all with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@gamestore.com");
                await EnsureRole(serviceProvider, adminID, "Admin");

                var clientID = await EnsureUser(serviceProvider, testUserPw, "client@gamestore.com");
                await EnsureRole(serviceProvider, clientID, "Client");

                var guestID = await EnsureUser(serviceProvider, testUserPw, "guest@gamestore.com");
                await EnsureRole(serviceProvider, guestID, "Guest");

                SeedDB(context);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context)
        {
            if (context.Product.Any())
            {
                return;   // DB has been seeded
            }

            context.Product.AddRange(
                new Product
                {
                    Name = "Forza Horizon 5",
                    Price = 79.99,
                    Description = "Blast off to the visually stunning, exhilarating Horizon Hot Wheels Park in the clouds high above Mexico. Race 10 amazing new cars on the fastest, most extreme tracks ever devised. Design, build, and share your own Hot Wheels adventure. Requires Forza Horizon 5 game, sold separately",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/08/24/forza-horizon-5-button-fin-1629830068379.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "Forza Horizon 4",
                    Price = 79.99,
                    Description = "Dynamic seasons change everything at the world’s greatest automotive festival. Go it alone or team up with others to explore beautiful and historic Britain in a shared open world.",
                    ImageURL = "https://assets-prd.ignimgs.com/2020/07/22/forza-horizon-4-button-fin-1595435190186.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "F1® 22",
                    Price = 79.99,
                    Description = "Enter the new era of Formula 1® in EA SPORTS™ F1® 22 the official videogame of the 2022 FIA Formula One World Championship™.",
                    ImageURL = "https://assets-prd.ignimgs.com/2022/04/21/f1-2022-1650559281245.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "Assetto Corsa",
                    Price = 22.79,
                    Description = "Assetto Corsa v1.16 introduces the new \"Laguna Seca\" laser-scanned track, 7 new cars among which the eagerly awaited Alfa Romeo Giulia Quadrifoglio! Check the changelog for further info!",
                    ImageURL = "https://assets1.ignimgs.com/2018/02/23/assetto-corsa---button-f-1519426077045.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "DiRT Rally 2.0",
                    Price = 22.79,
                    Description = "DiRT Rally 2.0 dares you to carve your way through a selection of iconic rally locations from across the globe, in the most powerful off-road vehicles ever made, knowing that the smallest mistake could end your stage.",
                    ImageURL = "https://assets1.ignimgs.com/2019/01/05/dirt-rally-2---button-fin-1546654027321.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "GRID Legends",
                    Price = 79.99,
                    Description= "GRID Legends delivers thrilling wheel-to-wheel motorsport action. Create dream race events, hop into live races, experience a dramatic virtual production story, and embrace the sensation of spectacular racing.",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/30/grid-legends-button-fin-1640849390531.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "Project CARS",
                    Price = 17.49,
                    Description= "THE ULTIMATE DRIVER JOURNEY delivers the soul of motor racing in the world’s most beautiful, authentic, and technically-advanced racing game.",
                    ImageURL= "https://assets1.ignimgs.com/2019/01/18/project-cars---button-fin-1547844094853.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "Need for Speed™ Hot Pursuit Remastered",
                    Price = 39.99,
                    Description= "Feel the thrill of the chase and the rush of escape behind the wheels of the world’s hottest high-performance cars in Need for Speed™ Hot Pursuit Remastered– a heart-pumping, socially competitive racing experience.",
                    ImageURL= "https://assets-prd.ignimgs.com/2020/10/05/need-for-speed-hot-pursuit-remastered-button-1601922609474.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "RIDE 3",
                    Price = 45.49,
                    Description= "Experience the most complete racing ever with RIDE 3! Race on different tracks all over the world, put your favourite vehicles' speed to the test and have fun by customising them with the new Livery Editor.",
                    ImageURL= "https://assets1.ignimgs.com/2018/05/18/ride-3---button-1-1526675096514.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "Turbo Golf Racing",
                    Price = 17.99,
                    Description = "Turbo Golf Racing is an arcade-style sports racing game for up to eight players online. Drive, boost, jump, flip and fly your Turbo-powered car. Slam into oversized golf balls. Race your friends in an explosive dash to the finish flag!",
                    ImageURL = "https://assets-prd.ignimgs.com/2022/08/03/turbo-golf-race-button-1659550595061.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.Racing
                },
                new Product
                {
                    Name = "BattleField 2042",
                    Price = 59.99,
                    Description = "Battlefield 2042 marks the return to the iconic all-out warfare of the franchise. Adapt \r\n\r\nand overcome dynamically changing battlegrounds with the help of your squad and: \r\n\r\ncutting-edge arsenal. With support for 128 players, prepare for unprecedented scale \r\n\r\non vast environments. Take on massive experiences, from updated multiplayer modes \r\n\r\nlike Conquest and Breakthrough to the all-new Battlefield ™ Hazard Zone. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/06/09/battlefield-2042-button-fin-1623262719242.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Destiny 2",
                    Price = 64.49,
                    Description = "Dive into the world of Destiny 2 to explore the mysteries of the solar system and experience responsive first-person shooter combat. Unlock powerful elemental abilities and collect unique gear to customize your Guardian's look and playstyle. Enjoy Destiny 2’s cinematic story, challenging co-op missions, and a variety of PvP modes alone or with friends. Download for free today and write your legend in the stars. ",
                    ImageURL = "https://assets1.ignimgs.com/2017/04/06/destiny-2---button2-1491517619460.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Rust",
                    Price = 50.49,
                    Description= "Rust is in its 8th year and has now had over 300 content updates, with a guaranteed content patch every month. From regular balance fixes and improvements to AI and graphics updates to adding content like new maps, musical instruments, trains and drones, as well as regular seasons and events, there’s always something interesting or dangerous (or both) happening on the island. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/07/rust-1638841834256.png?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Hunt: Showdown",
                    Price = 54.99,
                    Description= "The year is 1895, and you are a Hunter tasked with eliminating the savage, nightmarish monsters that have infested the Louisiana Bayou. Play alone or in teams of two or three, as you search for clues to help you track your target and compete against other Hunters who are after the same reward. Kill and banish your target, collect the bounty, and then get ready for the showdown; once the bounty is in your hands every other Hunter on the map will be after your prize. Show no mercy as you fight through a dark, ruthless world with brutal, period-inspired weapons, as you level up, unlock gear, and collect experience and gold for your Bloodline. ",
                    ImageURL= "https://assets1.ignimgs.com/2018/08/27/hunt-showdown-button-1-1535410734007.jpg?width=300&crop=1%3A1%2Csmart",
                    Category= Category.FPS
                },
                new Product
                {
                    Name = "Left 4 Dead 2",
                    Price = 11.49,
                    Description= "Set in the zombie apocalypse, Left 4 Dead 2 (L4D2) is the highly anticipated sequel to the award-winning Left 4 Dead, the #1 co-op game of 2008. \r\n\r\nThis co-operative action horror FPS takes you and your friends through the cities, swamps and cemeteries of the Deep South, from Savannah to New Orleans across five expansive campaigns. ",
                    ImageURL= "https://assets-prd.ignimgs.com/2021/12/10/left4dead2-1639126647529.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Apex Legends™",
                    Price = 54.99,
                    Description = "Conquer with character in Apex Legends, a free-to-play* Hero shooter where legendary characters with powerful abilities team up to battle for fame & fortune on the fringes of the Frontier. ",
                    ImageURL = "https://assets1.ignimgs.com/2019/02/04/apex-legends---button-fin-1549319070496.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "DayZ",
                    Price = 59.99,
                    Description = "There are no map markers, daily quests, or scoreboards to help you create your story. There is only Chernarus – 230 square kilometers of post-Soviet country that was struck by an unknown virus, which turned the majority of its population into raging infected. \r\n\r\nYour task? To survive the collapse of civilization for as long as you possibly can. Keep in mind that death is permanent in unforgiving Chernarus. All you’ll have when you start over again are the memories of your final mistake. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/20/dayz-1640044421966.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Squad",
                    Price = 59.99,
                    Description = "Squad’s newest map, Black Coast, features an aircraft carrier main base and focuses on highlighting the new amphibious mechanics. Five existing maps have been updated to use the new amphibious mechanics making ormerly impassable terrain including rivers, lakes, and ocean, can now be crossed with faction amphibious vehicles, allowing critical map locations to be flanked. ",
                    ImageURL = "https://assets2.ignimgs.com/2015/12/17/squad-buttonjpg-19bbc6.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                }, 
                new Product
                {
                    Name = "Hell Let Loose",
                    Price = 30.47,
                    Description = "Fight in the most iconic battles of the Western Front, including Carentan, Omaha Beach and Foy and more. This is combat at a whole new scale....with lumbering tanks dominating the battlefield, crucial supply chains fuelling the frontlines, you are a cog in the machine of colossal combined arms warfare. Hell Let Loose puts you in the chaos of war, complete with deep player-controlled vehicles, a dynamically evolving front line, and crucial unit-focused gameplay that commands the tide of battle. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/17/hell-let-loose-button-fin-1639779627909.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                },
                new Product
                {
                    Name = "Borderlands 3",
                    Price = 19.99,
                    Description = "The original shooter-looter returns, packing bazillions of guns and an all-new mayhem-fueled adventure! Blast through new worlds and enemies as one of four brand new Vault Hunters – the ultimate treasure-seeking badasses of the Borderlands, each with deep skill trees, abilities, and customization. Play solo or join with friends to take on insane enemies, score loads of loot and save your home from the most ruthless cult leaders in the galaxy.",
                    ImageURL = "https://assets1.ignimgs.com/2019/08/13/borderlands-3---button-fin-1565657702370.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.FPS
                }, 
                new Product
                {
                    Name = "The Elder Scrolls V: Skyrim",
                    Price = 53.49,
                    Description = " Winner of more than 200 Game of the Year Awards, Skyrim Special Edition brings the epic fantasy to life in stunning detail. The Special Edition includes the critically acclaimed game and add-ons with all-new features like remastered art and effects, volumetric god rays, dynamic depth of field, screen-space",
                    ImageURL = "https://assets-prd.ignimgs.com/2022/01/12/skyrimvr-sq-1642027863420.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Final Fantasy XIV Online ",
                    Price = 24.99,
                    Description = "Take part in an epic and ever-changing FINAL FANTASY as you adventure and explore with friends from around the world. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/07/ff14online-1638841900910.png?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Fallout 4 ",
                    Price = 26.99,
                    Description = "Bethesda Game Studios, the award-winning creators of Fallout 3 and The Elder Scrolls V: Skyrim, welcome you to the world of Fallout 4 – their most ambitious game ever, and the next generation of open-world gaming. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/07/fallout4-1638841806342.png?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Baldur's Gate 3 ",
                    Price = 79.99,
                    Description = "Gather your party, and return to the Forgotten Realms in a tale of fellowship and betrayal, sacrifice and survival, and the lure of absolute power. ",
                    ImageURL = "https://assets1.ignimgs.com/2019/06/08/baldurs-gate-3-button-02a-1559953190283.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "STAR WARS™: The Old Republic™",
                    Price = 22.79,
                    Description = "STAR WARS™: The Old Republic™ is a free-to-play MMORPG that puts you at the center of your own story-driven saga. Play as a Jedi, Sith, Bounty Hunter, or one of many other iconic STAR WARS roles in the galaxy far, far away over three thousand years before the classic films.",
                    ImageURL = "https://assets-prd.ignimgs.com/2022/01/27/swotor-sq1-1643302998212.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "FINAL FANTASY VII REMAKE",
                    Price = 89.99,
                    Description = "Cloud Strife, an ex-SOLDIER operative, descends on the mako-powered city of Midgar. The world of the timeless classic FINAL FANTASY VII is reborn, using cutting-edge graphics technology, a new battle system and an additional adventure featuring Yuffie Kisaragi. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/02/26/ff7-remake-integrade-button-1614308217803.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "The Witcher® 3: Wild Hunt ",
                    Price = 79.99,
                    Description = "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/08/witcher3-1638987659679.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Tales of Arise ",
                    Price = 59.49,
                    Description = "300 years of tyranny. A mysterious mask. Lost pain and memories. Wield the Blazing Sword and join a mysterious, untouchable girl to fight your oppressors. Experience a tale of liberation, featuring characters with next-gen graphical expressiveness! ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/12/21/tales-of-arise-button-fin-1640046637442.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Mass Effect™ Legendary Edition ",
                    Price = 79.99,
                    Description = "The Mass Effect™ Legendary Edition includes single-player base content and over 40 DLC from the highly acclaimed Mass Effect, Mass Effect 2, and Mass Effect 3 games, including promo weapons, armors, and packs — remastered and optimized for 4K Ultra HD. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2021/02/04/mass-effect-legendary-edition-button-1612480958351.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                },
                new Product
                {
                    Name = "Fable Anniversary ",
                    Price = 45.49,
                    Description = "FOR EVERY CHOICE, A CONSEQUENCE.Fully re-mastered with HD visuals and audio, Fable Anniversary is a stunning rendition of the original game that will delight faithful fans and new players alike! The all new Heroic difficulty setting will test the mettle of even the most hardcore Fable fan. ",
                    ImageURL = "https://assets-prd.ignimgs.com/2022/01/07/fable-anniversary-button-crop-1641529649779.jpg?width=300&crop=1%3A1%2Csmart",
                    Category = Category.RPG
                }
                );
            context.SaveChanges();
        }
    }
}
