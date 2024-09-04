using Microsoft.AspNetCore.Mvc;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
        [Route("[controller]")]
    public class DataSeedController : ControllerBase
    {
        public DataSeedController(){}

        [HttpPost]
        [Route("DataSeed")]
        public IActionResult DataSeed()
        {
            /*
            use VideoStreamingPlatform

insert into VideoStreamingPlatform.dbo.[User] (name,surname,userName,birthDate,country,subscriberCount,typeID)
values ('azer','pilavdzic','azer123','2000-09-26','Bosnia and Herzegovina',300,1)

insert into Video(videoName,filePath,description,resolutionType,uploadDate,durationInSecondes,isFree,userID,categoryID)
values 

('Exploring Street Food in Asia', 'https://www.youtube.com/watch?v=abc333', 'A journey through Asia’s best street food.', 'FULL_HD', GETDATE(), 20, 1, 1, 2),  -- Food
('Top 10 Indie Games 2024', 'https://www.youtube.com/watch?v=def444', 'Discover the top indie games coming in 2024.', 'FULL_HD', GETDATE(), 35, 1, 1, 3),  -- Gaming
('How to Style for Summer 2024', 'https://www.youtube.com/watch?v=ghi555', 'Get the latest summer fashion trends for 2024.', 'FULL_HD', GETDATE(), 15, 1, 1, 4),  -- Beauty and Fashion
('Best Concerts of the Year', 'https://www.youtube.com/watch?v=jkl666', 'Relive the best concerts of 2024.', 'FULL_HD', GETDATE(), 40, 1, 1, 5),  -- Music
('The Rise of AI in Healthcare', 'https://www.youtube.com/watch?v=mno777', 'How AI is transforming the healthcare industry.', 'FULL_HD', GETDATE(), 50, 1, 1, 6),  -- Science and Technology
('Hidden Gems of Europe', 'https://www.youtube.com/watch?v=pqr888', 'Explore hidden travel gems across Europe.', 'FULL_HD', GETDATE(), 60, 1, 1, 7),  -- Travel
('Mastering the Art of Italian Cuisine', 'https://www.youtube.com/watch?v=abc123',
'Learn how to prepare authentic Italian dishes from scratch, from pasta to pizza, with tips from world-renowned chefs.', 
'FULL_HD', GETDATE(), 50, 1, 1, 2),  -- CategoryId for Food

('Top 10 Open World Games in 2024', 'https://www.youtube.com/watch?v=def456',
'Explore the vast and immersive open-world games releasing in 2024, featuring stunning landscapes, compelling narratives, and endless gameplay.', 
'FULL_HD', GETDATE(), 35, 1, 1, 3),  -- CategoryId for Gaming

('The Beauty Secrets You Need to Know', 'https://www.youtube.com/watch?v=ghi789',
'Uncover the top beauty hacks and fashion tips from the industry experts, and elevate your personal style.', 
'FULL_HD', GETDATE(), 25, 1, 1, 4),  -- CategoryId for Beauty and Fashion

('Music Legends: The Story Behind the Hits', 'https://www.youtube.com/watch?v=jkl101',
'Dive into the history of music’s greatest hits and discover how these legendary tracks came to be, from inception to worldwide success.', 
'FULL_HD', GETDATE(), 40, 1, 1, 5),  -- CategoryId for Music

('The Future of Space Exploration', 'https://www.youtube.com/watch?v=mno112',
'Explore the latest advancements in space technology and discover how humanity is preparing for interplanetary travel in the near future.', 
'FULL_HD', GETDATE(), 55, 1, 1, 6),  -- CategoryId for Science and Technology

('Adventure in the Amazon Rainforest', 'https://www.youtube.com/watch?v=pqr113',
'Join us as we embark on an epic journey through the Amazon rainforest, uncovering its hidden wonders and exotic wildlife.', 
'FULL_HD', GETDATE(), 60, 1, 1, 7),  -- CategoryId for Travel

('The Ultimate Entertainment Experience', 'https://www.youtube.com/watch?v=stu114',
'Discover the best in entertainment, from movies to live shows, and everything in between in this comprehensive entertainment guide.', 
'FULL_HD', GETDATE(), 45, 1, 1, 1),  -- CategoryId for Entertainment

('The Science of Climate Change', 'https://www.youtube.com/watch?v=vwx115',
'Understand the science behind climate change, its impact on the world, and what steps we can take to combat it.', 
'FULL_HD', GETDATE(), 30, 1, 1, 6),  -- CategoryId for Science and Technology

('Epic Travel Adventures: Across the Globe', 'https://www.youtube.com/watch?v=yzx116',
'Join us as we travel to some of the world’s most exotic locations, from the deserts of Africa to the islands of the Pacific.', 
'FULL_HD', GETDATE(), 70, 1, 1, 7),  -- CategoryId for Travel

('Behind the Scenes of eSports Tournaments', 'https://www.youtube.com/watch?v=abx117',
'Go behind the scenes of the world’s biggest eSports tournaments and meet the pro players competing for the championship titles.', 
'FULL_HD', GETDATE(), 40, 1, 1, 3),  -- CategoryId for Gaming

('Enter the savage kingdom - wildlife LIVE', 'https://www.youtube.com/watch?v=BJ3Yv572V1A',
'Ruthless predators and powerful prey are embroiled in rivalry, betrayal, and battle, in a never-ending crusade for survival
, with characters so wild and ambitious and conflict so cutthroat, that no diction can do it justice. 
Each episode presents the battle from one characters point of view exposing the front lines of clan warfare— from a mothers sacrifice to a 
fathers rage at his own son','FULL_HD',getdate(), 120,1,1,1),

('The Ultimate Guide to Street Food', 'https://www.youtube.com/watch?v=123abc',
'Join us as we explore the hidden gems of street food around the world, showcasing mouth-watering dishes, unique flavors, and the vibrant cultures that create them.', 
'FULL_HD', GETDATE(), 30, 1, 1, 2),

('Top 10 Gaming Moments of All Time', 'https://www.youtube.com/watch?v=456def',
'From legendary eSports moments to game-breaking glitches, these are the top 10 gaming moments that will forever be etched in history.', 
'FULL_HD', GETDATE(), 25, 1, 1, 3),

('Exploring the Future of AI and Robotics', 'https://www.youtube.com/watch?v=789ghi',
'Discover how cutting-edge AI and robotics are reshaping industries and what the future holds for humanity in this technological revolution.', 
'FULL_HD', GETDATE(), 45, 1, 1, 6),

('The Ultimate Fashion Trends for 2024', 'https://www.youtube.com/watch?v=101jkl',
'Step into the world of high fashion as we unveil the biggest trends that are taking over the runway in 2024. From bold colors to unique textures, get ready for a fashion revolution.', 
'FULL_HD', GETDATE(), 15, 1, 1,4),

('Top Travel Destinations of 2024', 'https://www.youtube.com/watch?v=112mno',
'Pack your bags and get ready to explore the top travel destinations of 2024! From hidden gems to popular tourist spots, we have you covered.', 
'FULL_HD', GETDATE(), 40, 1, 1, 7),

('Top 10 Iconic Music Hits of the 90s', 'https://www.youtube.com/watch?v=113pqr',
'Relive the golden era of music as we count down the top 10 iconic hits from the 90s that changed the music industry forever.', 
'FULL_HD', GETDATE(), 20, 1, 1, 5),

('Inside the World of Virtual Reality Entertainment', 'https://www.youtube.com/watch?v=114stu',
'Explore the rapidly expanding world of virtual reality entertainment. From gaming to interactive experiences, VR is changing how we consume media.', 
'FULL_HD', GETDATE(), 35, 1, 1, 1)

insert into advertisement (userID,videoID,advertisementPictureURL)
values
(2, 2, 'https://i.ytimg.com/vi/emmXWi4iAW4/maxresdefault.jpg'),
(2, 4, 'https://imageio.forbes.com/specials-images/imageserve/628b8de7a18d8436b8782e88/0x0.jpg?format=jpg&height=600&width=1200&fit=bounds'),
(2, 2, 'https://i.ytimg.com/vi/emmXWi4iAW4/maxresdefault.jpg'),
(2, 4, 'https://imageio.forbes.com/specials-images/imageserve/628b8de7a18d8436b8782e88/0x0.jpg?format=jpg&height=600&width=1200&fit=bounds'),
(1, 5, 'https://cdn.pixabay.com/photo/2016/11/14/03/16/advertisement-1820919_960_720.jpg'),
(2, 7, 'https://i.ytimg.com/vi/abcd1234/maxresdefault.jpg'),
(1, 8, 'https://www.advertisementjpg.com/wp-content/uploads/2021/07/Adver-600x400.jpg'),
(2, 9, 'https://cdn.pixabay.com/photo/2018/02/10/21/44/advertisement-3146411_960_720.jpg'),
(1, 11, 'https://upload.wikimedia.org/wikipedia/commons/1/1e/Ad-image.png'),
(2, 13, 'https://www.advertisehere.com/advertise.jpg'),
(1, 14, 'https://i.ytimg.com/vi/xyz45678/maxresdefault.jpg'),
(2, 15, 'https://example.com/adver_pic_large.jpg'),
(1, 16, 'https://cdn.pixabay.com/photo/2020/05/22/10/15/advertisement-5204925_960_720.jpg'),
(2, 10, 'https://upload.wikimedia.org/wikipedia/commons/9/9d/Ad-sample.jpg');



insert into Blog(content,pictureURL,title,userID)
values 
('this is content for blog no1.','https://img.freepik.com/free-photo/teamwork-making-online-blog_53876-94868.jpg?','title for blog number 1',2),
('this is content for blog no2.','https://img.freepik.com/free-photo/technology-communication-icons-symbols-concept_53876-120314.jpg','title for blog number 2',2)
('this is content for blog no3.', 'https://img.freepik.com/free-photo/digital-marketing-media-technology-concept_53876-120315.jpg', 'title for blog number 3', 2),
('this is content for blog no4.', 'https://img.freepik.com/free-photo/creative-process-ideas-innovation-technology_53876-120316.jpg', 'title for blog number 4', 2),
('this is content for blog no5.', 'https://img.freepik.com/free-photo/music-notes-symbol-tech-background_53876-120317.jpg', 'title for blog number 5', 2),
('this is content for blog no6.', 'https://img.freepik.com/free-photo/science-research-symbols-icons-background_53876-120318.jpg', 'title for blog number 6', 2),
('this is content for blog no7.', 'https://img.freepik.com/free-photo/travel-symbols-concept-icons-background_53876-120319.jpg', 'title for blog number 7', 2),
('this is content for blog no8.', 'https://img.freepik.com/free-photo/entertainment-symbols-background-concept_53876-120320.jpg', 'title for blog number 8', 2),
('this is content for blog no9.', 'https://img.freepik.com/free-photo/delicious-food-cuisine-symbols_53876-120321.jpg', 'title for blog number 9', 2),
('this is content for blog no10.', 'https://img.freepik.com/free-photo/gaming-symbol-tech-background_53876-120322.jpg', 'title for blog number 10', 2),
('this is content for blog no11.', 'https://img.freepik.com/free-photo/fashion-symbols-concept-background_53876-120323.jpg', 'title for blog number 11', 2),
('this is content for blog no12.', 'https://img.freepik.com/free-photo/music-symbol-tech-background-concept_53876-120324.jpg', 'title for blog number 12', 2),
('this is content for blog no13.', 'https://img.freepik.com/free-photo/science-research-icons-symbols-background_53876-120325.jpg', 'title for blog number 13', 2),
('this is content for blog no14.', 'https://img.freepik.com/free-photo/exploring-new-travel-destinations-concept_53876-120326.jpg', 'title for blog number 14', 2),
('this is content for blog no15.', 'https://img.freepik.com/free-photo/entertainment-symbol-icons-background-concept_53876-120327.jpg', 'title for blog number 15', 2);



INSERT INTO EmojiShow (emojiName, ClickCounter, videoID)
VALUES
('Happy', 5, 1),
('Laugh', 10, 2),
('Love', 8, 3),
('Cool', 3, 4),
('Cry', 7, 5),
('Angry', 2, 6),
('ThumbsUp', 12, 7),
('ThumbsDown', 4, 8),
('Heart', 15, 9),
('Fire', 6, 10);

--groupMemberId is redundant, GroupMemberId is same as UserId
insert into GroupMembers(GroupID,GroupMemberID,UserID)
values 
(1,1,1),
(1,2,2)


--synchronization == group? must rename these values
insert into synchronization(GroupCode,SyncOwnerID,VideoID)
values ('abc123',1,3)


INSERT INTO playlist (userID, playlistName, isPublic)
VALUES
(1, 'Epic Travel Adventures', 1),
(2, 'Gaming Highlights', 0),
(1, 'Top Food Recipes', 1),
(2, 'Science and Tech Innovations', 1),
(1, 'Fashion Trends 2024', 0),
(2, 'Best Entertainment Moments', 1),
(1, 'Music Legends Collection', 1),
(2, 'VR and Gaming Experience', 0),
(1, 'Explore the World', 1),
(2, 'Tech Innovations Playlist', 0);





INSERT INTO playlistGroup (playlistID, videoID)
VALUES
-- Playlist 1: 'Epic Travel Adventures'
(1, 7),  -- Travel Soundtrack
(1, 10), -- Explore the World
(1, 16), -- Hidden Gems of Europe

-- Playlist 2: 'Gaming Highlights'
(2, 3),  -- Top 10 Gaming Moments of All Time
(2, 8),  -- VR and Gaming Experience
(2, 12), -- Top 10 Indie Games 2024

-- Playlist 3: 'Top Food Recipes'
(3, 2),  -- The Ultimate Guide to Street Food
(3, 11), -- Exploring Street Food in Asia

-- Playlist 4: 'Science and Tech Innovations'
(4, 6),  -- Exploring the Future of AI and Robotics
(4, 14), -- The Rise of AI in Healthcare

-- Playlist 5: 'Fashion Trends 2024'
(5, 4),  -- The Ultimate Fashion Trends for 2024
(5, 13), -- How to Style for Summer 2024

-- Playlist 6: 'Best Entertainment Moments'
(6, 1),  -- Inside the World of Virtual Reality Entertainment
(6, 15), -- Best Concerts of the Year

-- Playlist 7: 'Music Legends Collection'
(7, 5),  -- Top 10 Iconic Music Hits of the 90s
(7, 9),  -- Music Legends: The Story Behind the Hits

-- Playlist 8: 'VR and Gaming Experience'
(8, 8),  -- VR and Gaming Experience
(8, 12), -- Top 10 Indie Games 2024

-- Playlist 9: 'Explore the World'
(9, 7),  -- Travel Soundtrack
(9, 10), -- Explore the World
(9, 16), -- Hidden Gems of Europe

-- Playlist 10: 'Tech Innovations Playlist'
(10, 6),  -- Exploring the Future of AI and Robotics
(10, 14); -- The Rise of AI in Healthcare


INSERT INTO reportType (reportTypeId, [type])
VALUES
(1, 'misinformation'),
(2, 'copyright'),
(3, 'spam'),
(4, 'hate speech');

INSERT INTO report (reportText, reportTypeId, userID, videoID, dateOfReport)
VALUES
('This video contains misleading information about the topic.', 1, 1, 5, GETDATE()),  -- Misinformation report
('The video is using copyrighted material without permission.', 2, 2, 9, GETDATE()),  -- Copyright violation
('This video is filled with spam links in the comments and description.', 3, 1, 7, GETDATE()),  -- Spam report
('The video contains hate speech targeting specific groups.', 4, 2, 3, GETDATE()),  -- Hate speech report
('This video is spreading false claims that can mislead viewers.', 1, 1, 12, GETDATE()),  -- Misinformation report
('The video promotes content that infringes on copyrights.', 2, 2, 16, GETDATE());  -- Copyright violation





             
             */


            return Ok();




            
        }


    }
}
