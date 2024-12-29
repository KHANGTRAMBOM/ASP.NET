USE TestDoAnASP

INSERT INTO Genre (Name) VALUES 
(N'Pop'),
(N'Rock'),
(N'Hip Hop'),
(N'EDM'),
(N'R&B'),
(N'Jazz'),
(N'Classical'),
(N'Blues'),
(N'Country'),
(N'Latin'),
(N'Reggae'),
(N'K-Pop'),
(N'J-Pop'),
(N'Indie'),
(N'Acoustic'),
(N'Soundtrack'),
(N'Chill'),
(N'Folk'),
(N'Lo-fi'),
(N'Children'),
(N'Nhạc Trẻ'),
(N'Nhạc Vàng'),
(N'Nhạc Cách Mạng'),
(N'Bolero'),
(N'Nhạc Hòa Tấu'),
(N'Rap');



select * from genre
select * from Artist
select * from Songs


-- Mỹ Tâm
UPDATE Songs SET  Image = N'chuabaogio_image.jpg' WHERE SongID = 18
UPDATE Songs SET AudioFile = N'daudetruongthanh_audio.mp3' WHERE SongID = 18
UPDATE Songs SET Image = N'dunghoi_image.jpg' WHERE Title = N'Đừng Hỏi Em';

SELECT * FROM ASPNETUSERS


INSERT INTO Artist (Name, Bio, Avatar, IsVerified, CreatedAt) VALUES 
(N'Sơn Tùng M-TP', N'Sơn Tùng M-TP là một ca sĩ, nhạc sĩ nổi tiếng người Việt Nam. Anh được biết đến với các bài hát hit như "Lạc Trôi" và "Em Của Ngày Hôm Qua".', 'sontung_avatar.jpg', 1, '2012-01-01'),
(N'Mỹ Tâm', N'Mỹ Tâm là ca sĩ, nhạc sĩ nổi tiếng với các bài hát tình cảm và có nhiều năm hoạt động trong ngành âm nhạc Việt Nam.', 'mytam_avatar.jpg', 1, '2000-06-01'),
(N'Trấn Thành', N'Trấn Thành là một nghệ sĩ nổi tiếng ở Việt Nam với vai trò MC, diễn viên hài và ca sĩ.', 'tranthanh_avatar.jpg', 1, '2005-09-01'),
(N'Hương Tràm', N'Hương Tràm là ca sĩ nổi tiếng với các ca khúc ballad và nhạc trẻ, cô đoạt giải quán quân "The Voice" mùa đầu tiên.', 'huongtram_avatar.jpg', 1, '2013-04-01'),
(N'Đen Vâu', N'Đen Vâu là một rapper nổi tiếng với những bài hát phản ánh thực tế cuộc sống, được yêu thích bởi giới trẻ.', 'denvau_avatar.jpg', 1, '2015-07-01'),
(N'Jack', N'Jack là một ca sĩ và nhạc sĩ trẻ, nổi tiếng với các ca khúc như "Bạc Phận" và "Sao Em Lặng Im".', 'jack_avatar.jpg', 1, '2018-01-01'),
(N'Lou Hoàng', N'Lou Hoàng là ca sĩ nổi tiếng với các ca khúc trẻ trung, sôi động như "Em Không Thể" và "Cảm Giác Lạ".', 'louhoang_avatar.jpg', 1, '2014-09-01'),
(N'OnlyC', N'OnlyC là nhạc sĩ và ca sĩ nổi tiếng với các bài hát hit và sản xuất âm nhạc, như "Anh Đang Ở Đâu Đấy Anh" và "Yêu Được Không".', 'onlyc_avatar.jpg', 1, '2012-05-01'),
(N'Karik', N'Karik là rapper nổi tiếng với những bài hát rap mạnh mẽ và lời ca ý nghĩa, như "Khi Ta Lớn" và "Một Lần Cuối".', 'karik_avatar.jpg', 1, '2010-03-01'),
(N'Trung Quân', N'Trung Quân là ca sĩ nổi tiếng với giọng hát truyền cảm và các bài hát ballad lãng mạn như "Lạ Lùng" và "Độc Thân".', 'trungquan_avatar.jpg', 1, '2013-06-01'),
(N'Ariana Grande', N'Ariana Grande là ca sĩ và diễn viên người Mỹ nổi tiếng với giọng hát cao và các ca khúc hit như "Thank U, Next" và "No Tears Left to Cry".', 'arianagrande_avatar.jpg', 1, '2010-08-01'),
(N'Ed Sheeran', N'Ed Sheeran là ca sĩ, nhạc sĩ người Anh nổi tiếng với các bài hát như "Shape of You" và "Perfect".', 'edsheeran_avatar.jpg', 1, '2011-03-01'),
(N'Taylor Swift', N'Taylor Swift là ca sĩ, nhạc sĩ người Mỹ nổi tiếng với các album nhạc country và pop như "Love Story" và "Bad Blood".', 'taylorswift_avatar.jpg', 1, '2006-10-01'),
(N'BTS', N'BTS là nhóm nhạc nam Hàn Quốc nổi tiếng với các bài hát K-Pop và các ca khúc toàn cầu như "Dynamite" và "Boy With Luv".', 'bts_avatar.jpg', 1, '2013-06-13'),
(N'Justin Bieber', N'Justin Bieber là ca sĩ người Canada nổi tiếng với các bài hát như "Baby" và "Peaches".', 'justinbieber_avatar.jpg', 1, '2009-11-01'),
(N'Billie Eilish', N'Billie Eilish là ca sĩ và nhạc sĩ người Mỹ nổi tiếng với phong cách âm nhạc độc đáo và các bài hát như "Bad Guy" và "Everything I Wanted".', 'billieeilish_avatar.jpg', 1, '2016-03-01');



INSERT INTO Albums (ArtistID, Title, CoverArt, ReleaseDate, GenreID) VALUES
(1, N'Lạc Trôi', N'lactroi_cover.jpg', '2017-02-08', 1),  -- Sơn Tùng M-TP, Pop
(2, N'Hãy Về Với Nhau', N'hayvevoinhau_cover.jpg', '2014-10-01', 1),  -- Mỹ Tâm, Pop
(3, N'Cánh Hồng Phai', N'canhhongphai_cover.jpg', '2020-03-15', 1),  -- Trấn Thành, Pop
(4, N'Em Gái Mưa', N'emgaimua_cover.jpg', '2017-03-01', 1),  -- Hương Tràm, Pop
(5, N'Bài Này Chill Phết', N'bainaychillphet_cover.jpg', '2019-04-01', 3),  -- Đen Vâu, Hip Hop
(6, N'Hồng Nhan', N'hognhan_cover.jpg', '2018-04-01', 1),  -- Jack, Pop 
(7, N'Em Không Thể', N'emkhongthe_cover.jpg', '2017-05-01', 1),  -- Lou Hoàng, Pop
(8, N'Anh Nhớ Em', N'anhnhoem_cover.jpg', '2016-07-01', 2),  -- OnlyC, Rock
(9, N'Bạn Đời', N'bandoi_cover.jpg', '2020-01-20', 26),  -- Karik, Rap (Album sửa lại theo yêu cầu)
(10, N'Vì Một Câu Nói', N'vimotcaunoi_cover.jpg', '2018-06-01', 1),  -- Trung Quân, Pop
(11, N'Thank U, Next', N'thankunext_cover.jpg', '2019-02-08', 1),  -- Ariana Grande, Pop
(12, N'Shape of You', N'shapeofyou_cover.jpg', '2017-03-03', 1),  -- Ed Sheeran, Pop
(13, N'Love Story', N'love_story_cover.jpg', '2008-09-15', 1),  -- Taylor Swift, Pop
(14, N'Dynamite', N'dynamite_cover.jpg', '2020-08-21', 2),  -- BTS, Rock
(15, N'Sorry', N'sorry_cover.jpg', '2015-11-09', 1),  -- Justin Bieber, Pop
(16, N'Bad Guy', N'badguy_cover.jpg', '2019-03-29', 1);  -- Billie Eilish, Pop



-- Sơn Tùng M-TP
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (1, 1, 1, N'Lạc Trôi', N'lactroi_audio.mp3', 260, 12000, 0, 'lactroi_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (1, 1, 1, N'Chúng Ta Của Hiện Tại', N'chungtacuahientai_audio.mp3', 240, 15000, 0, 'chungtacuahientai_image.jpg');

-- Mỹ Tâm
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (2, 2, 1, N'Hãy Về Với Nhau', N'hayveveinhau_audio.mp3', 230, 13000, 0, 'hayvevoinhau_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (2, 2, 1, N'Đừng Hỏi Em', N'dunghoi_audio.mp3', 210, 12000, 0, 'dunghoi_image.jpg');

-- Trấn Thành
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (3, 3, 1, N'Cánh Hồng Phai', N'canhhongphai_audio.mp3', 230, 7000, 0, 'canhhongphai_image.jpg');

-- Hương Tràm
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (4, 4, 1, N'Em Gái Mưa', N'emgaimua_audio.mp3', 240, 9000, 0, 'emgaimua_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (4, 4, 1, N'Duyên Mình Lỡ', N'duyenminlo_audio.mp3', 250, 8000, 0, 'duyenminlo_image.jpg');

-- Đen Vâu
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (5, 5, 3, N'Bài Này Chill Phết', N'bainaychillphet_audio.mp3', 260, 11000, 0, 'bainaychillphet_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (5, 5, 3, N'Lối Nhỏ', N'loinho_audio.mp3', 280, 14000, 0, 'loinho_image.jpg');

-- Jack
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (6, 6, 1, N'Hồng Nhan', N'baihongnhan_audio.mp3', 230, 9000, 0, 'hongnhan_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (6, 6, 1, N'Sóng Gió', N'songgio_audio.mp3', 240, 9500, 0, 'songgio_image.jpg');

-- Lou Hoàng
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (7, 7, 1, N'Em Không Thể', N'emkhongthe_audio.mp3', 220, 8500, 0, 'emkhongthe_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (7, 7, 1, N'Yêu Một Người Có Lẽ', N'yeumotnguoicole_audio.mp3', 250, 10000, 0, 'yeumotnguoicole_image.jpg');

-- OnlyC
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (8, 8, 2, N'Anh Nhớ Em', N'anhnhoem_audio.mp3', 240, 13000, 0, 'anhnhoem_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (8, 8, 2, N'Yêu Là Tha Thu', N'yeulathathu_audio.mp3', 230, 11000, 0, 'yeulathathu_image.jpg');

-- Karik
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (9, 9, 26, N'Bạn Đời', N'bandoi_audio.mp3', 240, 12000, 0, 'bandoi_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (9, 9, 26, N'Người Lạ Ơi', N'nguoilaoi_audio.mp3', 220, 13000, 1, 'nguoilaoi_image.jpg');

-- Trung Quân
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (10, 10, 1, N'Vì Một Câu Nói', N'vimotcaunoi_audio.mp3', 250, 9000, 0, 'vimotcaunoi_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (10, 10, 1, N'Dấu Mưa', N'daumua_audio.mp3', 240, 10000, 0, 'daumua_image.jpg');

-- Ariana Grande
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (11, 11, 1, N'Thank U, Next', N'thankunext_audio.mp3', 240, 15000, 0, 'thankunext_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (11, 11, 1, N'7 Rings', N'7rings_audio.mp3', 230, 12000, 0, '7rings_image.jpg');

-- Ed Sheeran
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (12, 12, 1, N'Shape of You', N'shapeofyou_audio.mp3', 260, 13000, 0, 'shapeofyou_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (12, 12, 1, N'Perfect', N'perfect_audio.mp3', 230, 14000, 0, 'perfect_image.jpg');

-- Taylor Swift
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (13, 13, 1, N'Love Story', N'lovestory_audio.mp3', 220, 15000, 0, 'lovestory_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (13, 13, 1, N'Blank Space', N'blankspace_audio.mp3', 230, 14000, 0, 'blankspace_image.jpg');

-- BTS
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (14, 14, 2, N'Dynamite', N'dynamite_audio.mp3', 250, 16000, 0, 'dynamite_image.jpg');

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image)
VALUES (14, 14, 2, N'Boy With Luv', N'boywithluv_audio.mp3', 240, 17000, 0, 'boywithluv_image');

-- Justin Bieber
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image) 
VALUES (15, 15, 1, N'Sorry', N'sorry_audio.mp3', 240, 13000, 0, 'sorry_image.jpg'); 

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image) 
VALUES (15, 15, 1, N'Love Yourself', N'loveyourself_audio.mp3', 220, 15000, 0, 'loveyourself_image.jpg'); 

-- Billie Eilish 
INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image) 
VALUES (16, 16, 1, N'Bad Guy', N'badguy_audio.mp3', 230, 14000, 0, 'badguy_image.jpg'); 

INSERT INTO Songs (AlbumID, ArtistID, GenreID, Title, AudioFile, Duration, PlayCount, IsExplicit, Image) 
VALUES (16, 16, 1, N'Everything I Wanted', N'everythingiwanted_audio.mp3', 240, 15000, 1, 'everythingiwanted_image.jpg');



-- Thêm vai trò 'Manager'
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (NEWID(), 'Manager', 'MANAGER');

-- Thêm vai trò 'Normal'
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (NEWID(), 'Normal', 'NORMAL');

-- Thêm vai trò 'Premium'
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (NEWID(), 'Premium', 'PREMIUM');



-- Thêm người dùng Manager
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, Avatar, CreatedAt, IsPremium, PremiumExpiry)
VALUES
(NEWID(), 'manager@example.com', 'MANAGER@EXAMPLE.COM', 'manager@example.com', 'MANAGER@EXAMPLE.COM', 1, 'manager01', NEWID(), NEWID(), NULL, 0, 0, NULL, 1, 0, 'avatar_manager.jpg', GETDATE(), 0, NULL);

-- Thêm người dùng Normal
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, Avatar, CreatedAt, IsPremium, PremiumExpiry)
VALUES
(NEWID(), 'normaluser@example.com', 'NORMALUSER@EXAMPLE.COM', 'normaluser@example.com', 'NORMALUSER@EXAMPLE.COM', 1, 'normal01', NEWID(), NEWID(), NULL, 0, 0, NULL, 1, 0, 'avatar_normaluser.jpg', GETDATE(), 0, NULL);

-- Thêm người dùng Premium
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, Avatar, CreatedAt, IsPremium, PremiumExpiry)
VALUES
(NEWID(), 'premiumuser@example.com', 'PREMIUMUSER@EXAMPLE.COM', 'premiumuser@example.com', 'PREMIUMUSER@EXAMPLE.COM', 1, 'premium01', NEWID(), NEWID(), NULL, 0, 0, NULL, 1, 0, 'avatar_premiumuser.jpg', GETDATE(), 1, '2025-12-31');


-- Lấy UserId của người dùng 'manager@example.com'
DECLARE @UserId NVARCHAR(450);
SELECT @UserId = Id FROM AspNetUsers WHERE UserName = 'manager@example.com';

-- Lấy RoleId của vai trò 'Manager'
DECLARE @RoleId NVARCHAR(450);
SELECT @RoleId = Id FROM AspNetRoles WHERE Name = 'Manager';

-- Gán vai trò 'Manager' cho người dùng
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@UserId, @RoleId);

-- Lấy UserId của người dùng 'normaluser@example.com'
SELECT @UserId = Id FROM AspNetUsers WHERE UserName = 'normaluser@example.com';

-- Lấy RoleId của vai trò 'Normal'
SELECT @RoleId = Id FROM AspNetRoles WHERE Name = 'Normal';

-- Gán vai trò 'Normal' cho người dùng
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@UserId, @RoleId);

-- Lấy UserId của người dùng 'premiumuser@example.com'
SELECT @UserId = Id FROM AspNetUsers WHERE UserName = 'premiumuser@example.com';

-- Lấy RoleId của vai trò 'Premium'
SELECT @RoleId = Id FROM AspNetRoles WHERE Name = 'Premium';

-- Gán vai trò 'Premium' cho người dùng
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@UserId, @RoleId);



DECLARE @UserNomal NVARCHAR(450);
DECLARE @UserPre NVARCHAR(450);
-- Lấy UserId của người dùng 'normaluser@example.com'
SELECT @UserNomal = Id FROM AspNetUsers WHERE UserName = 'normaluser@example.com';

-- Lấy UserId của người dùng 'premiumuser@example.com'
SELECT @UserPre = Id FROM AspNetUsers WHERE UserName = 'premiumuser@example.com';

select playlists.Title , a.UserName from playlists,aspnetusers a  where Playlists.UserID = a.Id 
select * from AspNetUsers
use TestDoAnASP
select * from aspnetusers

select * from playlistsongs
select * from aspnetroles
select * from aspnetusers
select * from aspnetuserroles

select * from songs

select * from playlists

DELETE FROM Playlists
WHERE Title LIKE N'%Playlist%'



UPDATE aspnetuserroles
SET PasswordHash = 'AQAAAAIAAYagAAAAEAhtDU6AeOI2SdccLNqn518ndXazv2Kq1mykIL2zWumOoB9gH9a8UNhlSoajxSZVJw=='
WHERE UserName = 'premiumuser@example.com';

UPDATE aspnetusers
SET PasswordHash = 'AQAAAAIAAYagAAAAEAhtDU6AeOI2SdccLNqn518ndXazv2Kq1mykIL2zWumOoB9gH9a8UNhlSoajxSZVJw=='
WHERE UserName = 'premiumuser@example.com';

-- Thêm Playlist cho người dùng Premium
INSERT INTO Playlists (UserID, Title, Description, IsPublic, CreatedAt, UpdatedAt)
VALUES
(@UserPre, N'Melodies of the Night', N'Những bản nhạc nhẹ nhàng, thư giãn dành cho những buổi tối lãng mạn.', 1, GETDATE(), GETDATE()),
(@UserPre, N'Premium Vibes', N'Playlist dành cho những ngày cuối tuần thư giãn, tận hưởng âm nhạc tuyệt vời.', 1, GETDATE(), GETDATE()),
(@UserPre, N'Epic Soundtracks', N'Những bài nhạc nền hùng tráng, phù hợp cho mọi cảm xúc và thời gian.', 1, GETDATE(), GETDATE());

-- Thêm Playlist cho người dùng Normal
INSERT INTO Playlists (UserID, Title, Description, IsPublic, CreatedAt, UpdatedAt)
VALUES
(@UserNomal, N'Nhạc Vui Vẻ Mỗi Ngày', N'Những ca khúc năng động để bắt đầu một ngày mới tràn đầy năng lượng.', 0, GETDATE(), GETDATE()),
(@UserNomal, N'Hòa Mình Vào Dòng Nhạc', N'Những bài hát nhẹ nhàng giúp bạn thư giãn sau những giờ làm việc căng thẳng.', 0, GETDATE(), GETDATE()),
(@UserNomal, N'Phút Giây Thư Thái', N'Playlist này sẽ đưa bạn vào không gian thư giãn, tạm rời xa mọi lo âu.', 0, GETDATE(), GETDATE());



-- PlaylistSongs cho Playlist "Melodies of the Night" của người dùng Premium
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(1, 1, 1, GETDATE()),  -- "Lạc Trôi" (SongID = 1)
(1, 2, 2, GETDATE()),  -- "Chúng Ta Của Hiện Tại" (SongID = 2)
(1, 3, 3, GETDATE()),  -- "Hãy Về Với Nhau" (SongID = 3)
(1, 4, 4, GETDATE()),  -- "Đừng Hỏi Em" (SongID = 4)
(1, 5, 5, GETDATE());  -- "Cánh Hồng Phai" (SongID = 5)

-- PlaylistSongs cho Playlist "Premium Vibes" của người dùng Premium
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(2, 6, 1, GETDATE()),  -- "Em Gái Mưa" (SongID = 6)
(2, 7, 2, GETDATE()),  -- "Duyên Mình Lỡ" (SongID = 7)
(2, 8, 3, GETDATE()),  -- "Bài Này Chill Phết" (SongID = 8)
(2, 9, 4, GETDATE()),  -- "Lối Nhỏ" (SongID = 9)
(2, 10, 5, GETDATE()); -- "Hồng Nhan" (SongID = 10)

-- PlaylistSongs cho Playlist "Epic Soundtracks" của người dùng Premium
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(3, 11, 1, GETDATE()),  -- "Sóng Gió" (SongID = 11)
(3, 12, 2, GETDATE()),  -- "Em Không Thể" (SongID = 12)
(3, 13, 3, GETDATE()),  -- "Yêu Một Người Có Lẽ" (SongID = 13)
(3, 14, 4, GETDATE()),  -- "Anh Nhớ Em" (SongID = 14)
(3, 15, 5, GETDATE()); -- "Yêu Là Tha Thu" (SongID = 15)

-- PlaylistSongs cho Playlist "Nhạc Vui Vẻ Mỗi Ngày" của người dùng Normal
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(4, 16, 1, GETDATE()),  -- "Bạn Đời" (SongID = 16)
(4, 17, 2, GETDATE()),  -- "Người Lạ Ơi" (SongID = 17)
(4, 18, 3, GETDATE()),  -- "Vì Một Câu Nói" (SongID = 18)
(4, 19, 4, GETDATE()),  -- "Dấu Mưa" (SongID = 19)
(4, 20, 5, GETDATE()); -- "Thank U, Next" (SongID = 20)

-- PlaylistSongs cho Playlist "Hòa Mình Vào Dòng Nhạc" của người dùng Normal
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(5, 21, 1, GETDATE()),  -- "7 Rings" (SongID = 21)
(5, 22, 2, GETDATE()),  -- "Shape of You" (SongID = 22)
(5, 23, 3, GETDATE()),  -- "Perfect" (SongID = 23)
(5, 24, 4, GETDATE()),  -- "Love Story" (SongID = 24)
(5, 25, 5, GETDATE()); -- "Blank Space" (SongID = 25)

-- PlaylistSongs cho Playlist "Phút Giây Thư Thái" của người dùng Normal
INSERT INTO PlaylistSongs (PlaylistID, SongID, Position, AddedAt)
VALUES
(6, 26, 1, GETDATE()),  -- "Dynamite" (SongID = 26)
(6, 27, 2, GETDATE()),  -- "Boy With Luv" (SongID = 27)
(6, 28, 3, GETDATE()),  -- "Sorry" (SongID = 28)
(6, 29, 4, GETDATE()),  -- "Love Yourself" (SongID = 29)
(6, 30, 5, GETDATE()); -- "Bad Guy" (SongID = 30)



DECLARE @Id NVARCHAR(450);
SELECT @Id = Id FROM AspNetUsers WHERE UserName = 'mynametanker@gmail.com';

UPDATE Likes SET UserID = @Id WHERE CreatedAt = '2024-12-07 10:57:35.4566667' OR  CreatedAt = '2024-12-07 10:57:35.4533333'

INSERT INTO Likes (UserID, SongID, CreatedAt)
VALUES
(@UserNomal, 1, GETDATE()),  -- "Lạc Trôi" (SongID = 1)
(@UserNomal, 5, GETDATE()),  -- "Cánh Hồng Phai" (SongID = 5)
(@UserNomal, 10, GETDATE()), -- "Hồng Nhan" (SongID = 10)
(@UserNomal, 16, GETDATE()), -- "Bạn Đời" (SongID = 16)
(@UserNomal, 20, GETDATE()); -- "Thank U, Next" (SongID = 20)

INSERT INTO Likes (UserID, SongID, CreatedAt)
VALUES
(@UserPre, 2, GETDATE()),  -- "Chúng Ta Của Hiện Tại" (SongID = 2)
(@UserPre, 7, GETDATE()),  -- "Duyên Mình Lỡ" (SongID = 7)
(@UserPre, 8, GETDATE()),  -- "Bài Này Chill Phết" (SongID = 8)
(@UserPre, 12, GETDATE()), -- "Em Không Thể" (SongID = 12)
(@UserPre, 23, GETDATE()); -- "Perfect" (SongID = 23)
