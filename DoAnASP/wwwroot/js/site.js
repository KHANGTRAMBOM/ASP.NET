// Script to handle "Create Playlist" button

/*Hát nhạc trong playlist*/

let currentPlaylist = [];  // Mảng chứa tất cả bài hát
let currentSongIndex = 0;  // Chỉ số của bài hát hiện tại trong playlist

// Hàm này sẽ được gọi khi nút play được nhấn
function playPlaylist(button) {
    // Làm trống playlist cũ và chỉ số hiện tại
    currentPlaylist = [];
    currentSongIndex = 0;

    // Lấy tất cả các bài hát từ playlist trong DOM
    document.querySelectorAll('.playlist-view-song-row').forEach(function (row) {
        const songData = {
            title: row.querySelector('.playlist-view-song-name').innerText,
            artist: row.querySelector('.playlist-view-song-artist').innerText,
            image: row.querySelector('img').src,
            audio: row.dataset.audio
        };
        currentPlaylist.push(songData);
    });

    // Bắt đầu phát bài hát đầu tiên trong playlist
    playSongFromPlaylist(currentSongIndex);
}

// Hàm để phát bài hát từ playlist
function playSongFromPlaylist(index) {
    isPlayingPlaylist = true;
    if (index < currentPlaylist.length) {
        const song = currentPlaylist[index];
        audioPlayer.src = song.audio;
        updateNowPlaying(song.title, song.artist, song.image); // Cập nhật Now Playing Bar
        audioPlayer.play();
        updatePlayPauseButton(true); // Cập nhật trạng thái nút play/pause
        currentSongIndex = index;
        savePlayerState(); // Lưu trạng thái người dùng
    }
}

// Khi bài hát kết thúc, phát bài tiếp theo
audioPlayer.addEventListener('ended', function () {
    if (currentSongIndex < currentPlaylist.length - 1) {
        playSongFromPlaylist(currentSongIndex + 1);
    } else {
        updatePlayPauseButton(false); // Dừng nếu hết playlist
    }
});

// Hàm này sẽ cập nhật thông tin bài hát trong Now Playing Bar
function updateNowPlaying(title, artist, image) {
    document.getElementById('nowPlayingTitle').innerText = title;
    document.getElementById('nowPlayingArtist').innerText = artist;
    document.getElementById('nowPlayingImage').src = image;
}

/* Nút tiền và lùi trong playlist*/
function playPrevious2() {
    if (currentSongIndex > 0) {
        currentSongIndex--; // Giảm chỉ số bài hát
        playSongFromPlaylist(currentSongIndex); // Phát bài hát trước đó
    } else {
        // Nếu đang ở bài hát đầu tiên, không làm gì cả
        console.log("Đã ở bài hát đầu tiên");
    }
}

function playNext2() {
    if (currentSongIndex < currentPlaylist.length - 1) {
        currentSongIndex++; // Tăng chỉ số bài hát
        playSongFromPlaylist(currentSongIndex); // Phát bài hát tiếp theo
    } else {
        // Nếu đang ở bài hát cuối cùng, không làm gì cả
        console.log("Đã ở bài hát cuối cùng");
    }
}



/* Tạo play list */
document.addEventListener('DOMContentLoaded', function () {
    const createPlaylistButton = document.getElementById('createPlaylistButton');

    if (createPlaylistButton) {
        createPlaylistButton.addEventListener('click', function () {
            const url = createPlaylistButton.getAttribute('data-url');
            const csrfTokenElement = document.querySelector('meta[name="__RequestVerificationToken"]');
            const csrfToken = csrfTokenElement ? csrfTokenElement.content : null;

            if (!csrfToken) {
                console.error('CSRF token not found');
                alert('Không tìm thấy CSRF token!');
                return;
            }

            console.log('Sending request to:', url);
            console.log('CSRF Token:', csrfToken);

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': csrfToken
                },
                credentials: 'include'
            })
                .then(response => {
                    console.log('Response status:', response.status);
                    console.log('Response headers:', Object.fromEntries(response.headers.entries()));
                    if (!response.ok) {
                        return response.text().then(text => {
                            throw new Error(`HTTP error! status: ${response.status}, body: ${text}`);
                        });
                    }
                    return response.text();
                })
                .then(text => {
                    console.log('Raw response:', text);
                    try {
                        return JSON.parse(text);
                    } catch (error) {
                        console.error('Error parsing JSON:', error);
                        throw new Error('Invalid JSON response');
                    }
                })
                .then(data => {
                    console.log('Parsed data:', data);

                    const playlistContainer = document.querySelector('.playlist-container');
                    if (!playlistContainer) {
                        throw new Error('Playlist container not found');
                    }

                    const newPlaylistElement = document.createElement('a');
                    newPlaylistElement.href = `/Home/Details2/${data.playlistID}`;
                    newPlaylistElement.classList.add('details-link2', 'playlist-item');
                    newPlaylistElement.innerHTML = `
                    <div class="playlist-icon">
                        <i class="bi bi-music-note-list"></i>
                    </div>
                    <div class="playlist-info">
                        <div class="playlist-title">${data.title}</div>
                        <div class="playlist-subtitle">Danh sách phát • ${data.userName.substring(0, data.userName.indexOf('@'))}</div>
                    </div>
                `;

                    playlistContainer.appendChild(newPlaylistElement);
                    alert('🔥🔥🔥🔥 Bạn đã tạo một Playlist 🎶🎶🎶🎶');

                })
                .catch(error => {
                    console.error('Lỗi chi tiết khi tạo playlist:', error);
                    alert('Vui lòng đăng nhập để tạo playlist của riêng bạn ❤️❤️❤️❤️❤️');
                });
        });
    } else {
        console.warn('Create playlist button not found');
    }
});

/* Tạo Likes */
document.addEventListener("DOMContentLoaded", function () {
    window.toggleLike = function (songId) {
        var url = document.getElementById('likeButton').getAttribute('data-url');

        $.ajax({
            url: url,
            type: 'POST',
            data: { songId: songId },
            success: function (response) {
                if (response.success) {
                    // Thay đổi lớp CSS của heartIcon
                    var heartIcon = document.getElementById('heartIcon-' + songId);
                    if (response.liked) {
                        heartIcon.classList.remove('bi-heart');
                        heartIcon.classList.add('bi-heart-fill', 'text-danger');
                    } else {
                        heartIcon.classList.remove('bi-heart-fill', 'text-danger');
                        heartIcon.classList.add('bi-heart');
                    }
                } else {
                    alert('Failed to update like status: ' + response.message);
                }
            },
            error: function () {
                alert('An error occurred while updating the like status.');
            }
        });
    };
});























