let currentlyPlaying = null;
let audioPlayer = document.getElementById('audioPlayer');
let playPauseButton = document.getElementById('playPauseButton');
let progressBar = document.getElementById('progressBar');
let currentTimeDisplay = document.getElementById('currentTime');
let durationDisplay = document.getElementById('duration');
let volumeSlider = document.querySelector('.volume-slider');
let volumeIcon = document.querySelector('.volume-icon i');
let isPlayingPlaylist = false;

audioPlayer.addEventListener('play', savePlayerState);
audioPlayer.addEventListener('pause', savePlayerState);
audioPlayer.addEventListener('timeupdate', function () {
    if (Math.abs(audioPlayer.currentTime - globalPlaybackState.currentTime) > 1) {
        savePlayerState();
    }
});

let globalPlaybackState = {
    isPlaying: false,
    currentTrack: null,
    currentTime: 0
};


function PlayMusic(button) {
    const card = button.closest('.phatnhac');
    const audioSrc = card.dataset.audio;
    const title = card.dataset.title;
    const artist = card.dataset.artist;
    const image = card.querySelector('img').src;
    const songId = card.dataset.id;

    if (currentlyPlaying === card) {
        if (audioPlayer.paused) {
            audioPlayer.play();
            updatePlayPauseButton(true);
        } else {
            audioPlayer.pause();
            updatePlayPauseButton(false);
        }
    } else {
        if (currentlyPlaying) {
            const prevButton = currentlyPlaying.querySelector('.play-button');
            prevButton.innerHTML = '<i class="bi bi-play-fill"></i>';
            currentlyPlaying.classList.remove('playing');
        }

        audioPlayer.src = audioSrc;
        audioPlayer.play();
        updatePlayPauseButton(true);
        card.classList.add('playing');
        currentlyPlaying = card;

        updateNowPlaying(title, artist, image);
        incrementPlayCount(songId);
    }

    // Lưu trạng thái sau khi thay đổi
    savePlayerState();
}
function togglePlay(button) {
    const card = button.closest('.media-card');
    const audioSrc = card.dataset.audio;
    const title = card.dataset.title;
    const artist = card.dataset.artist;
    const image = card.querySelector('img').src;
    const songId = card.dataset.id;

    if (currentlyPlaying === card) {
        if (audioPlayer.paused) {
            audioPlayer.play();
            updatePlayPauseButton(true);
        } else {
            audioPlayer.pause();
            updatePlayPauseButton(false);
        }
    } else {
        if (currentlyPlaying) {
            const prevButton = currentlyPlaying.querySelector('.play-button');
            prevButton.innerHTML = '<i class="bi bi-play-fill"></i>';
            currentlyPlaying.classList.remove('playing');
        }

        audioPlayer.src = audioSrc;
        audioPlayer.play();
        updatePlayPauseButton(true);
        card.classList.add('playing');
        currentlyPlaying = card;

        updateNowPlaying(title, artist, image);
        incrementPlayCount(songId);
    }

    // Lưu trạng thái sau khi thay đổi
    savePlayerState();
}


// Thêm event listener cho popstate để xử lý navigation back/forward
window.addEventListener('popstate', function (event) {
    location.reload();
});

function incrementPlayCount(songId) {
    fetch(`/Home/TangLX/${songId}`, { method: 'POST' })
        .then(response => response.json())
        .then(newPlayCount => {
            console.log(`Play count for song ${songId} increased to ${newPlayCount}`);
        })
        .catch(error => console.error('Error:', error));
}

function updateNowPlaying(title, artist, image) {
    document.getElementById('nowPlayingTitle').textContent = title;
    document.getElementById('nowPlayingArtist').textContent = artist;
    document.getElementById('nowPlayingImage').src = image;
}


function updatePlayPauseButton(isPlaying) {
    playPauseButton.innerHTML = isPlaying ? '<i class="bi bi-pause-fill"></i>' : '<i class="bi bi-play-fill"></i>';
}

function formatTime(seconds) {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = Math.floor(seconds % 60);
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
}

audioPlayer.addEventListener('timeupdate', function () {
    const progress = (audioPlayer.currentTime / audioPlayer.duration) * 100;
    progressBar.style.width = `${progress}%`;
    currentTimeDisplay.textContent = formatTime(audioPlayer.currentTime);
});

audioPlayer.addEventListener('loadedmetadata', function () {
    durationDisplay.textContent = formatTime(audioPlayer.duration);
});

audioPlayer.addEventListener('ended', function () {
    if (currentlyPlaying) {
        const button = currentlyPlaying.querySelector('.play-button');
        button.innerHTML = '<i class="bi bi-play-fill"></i>';
        currentlyPlaying.classList.remove('playing');
        currentlyPlaying = null;
    }
    updatePlayPauseButton(false);
});

playPauseButton.addEventListener('click', function () {
    if (audioPlayer.paused) {
        audioPlayer.play();
        updatePlayPauseButton(true);
    } else {
        audioPlayer.pause();
        updatePlayPauseButton(false);
    }
});

document.querySelector('.progress-bar').addEventListener('click', function (e) {
    const percent = e.offsetX / this.offsetWidth;
    audioPlayer.currentTime = percent * audioPlayer.duration;
});

function playPrevious() {
    if (!isPlayingPlaylist) {
        if (currentlyPlaying) {
            const prevCard = currentlyPlaying.previousElementSibling;
            if (prevCard && prevCard.classList.contains('media-card')) {
                const prevButton = prevCard.querySelector('.play-button');
                togglePlay(prevButton);
            }
        }
    } else {
        playPrevious2(); 
    }

}

function playNext() {
    if (!isPlayingPlaylist) {
        if (currentlyPlaying) {
            const nextCard = currentlyPlaying.nextElementSibling;
            if (nextCard && nextCard.classList.contains('media-card')) {
                const nextButton = nextCard.querySelector('.play-button');
                togglePlay(nextButton);
            }
        }
    } else {
        playNext2();
    }

}

document.getElementById('prevButton').addEventListener('click', playPrevious);
document.getElementById('nextButton').addEventListener('click', playNext);

function updateVolumeSlider(volume) {
    volumeSlider.value = volume * 100;
    const percentage = volume * 100;
    volumeSlider.style.background = `linear-gradient(to right, #1db954 0%, #1db954 ${percentage}%, #535353 ${percentage}%, #535353 100%)`;
}

volumeSlider.addEventListener('input', function () {
    const volumePercent = this.value / 100;
    audioPlayer.volume = volumePercent;
    updateVolumeIcon(volumePercent);
    updateVolumeSlider(volumePercent);
});

function updateVolumeIcon(volumePercent) {
    if (volumePercent === 0) {
        volumeIcon.className = 'bi bi-volume-mute';
    } else if (volumePercent < 0.5) {
        volumeIcon.className = 'bi bi-volume-down';
    } else {
        volumeIcon.className = 'bi bi-volume-up';
    }
}


// Initialize volume
audioPlayer.volume = volumeSlider.value / 100;
updateVolumeIcon(audioPlayer.volume);
updateVolumeSlider(audioPlayer.volume);

// Mute/Unmute
document.querySelector('.volume-icon').addEventListener('click', function () {
    if (audioPlayer.volume > 0) {
        audioPlayer.dataset.lastVolume = audioPlayer.volume;
        audioPlayer.volume = 0;
    } else {
        const lastVolume = parseFloat(audioPlayer.dataset.lastVolume) || 1;
        audioPlayer.volume = lastVolume;
    }
    updateVolumeIcon(audioPlayer.volume);
    updateVolumeSlider(audioPlayer.volume);
});

audioPlayer.addEventListener('volumechange', function () {
    updateVolumeSlider(audioPlayer.volume);
    updateVolumeIcon(audioPlayer.volume);
});


// Nút lùi lại
window.addEventListener('popstate', function (e) {
    // Kiểm tra nếu trang hiện tại là SearchEmpty
    if (window.location.pathname === "/searchEmpty") {
        // Tải lại dữ liệu từ model SongsAndAlbumsViewModel
        loadSearchEmptyLayout();
    }
});

// Hàm để tải nội dung SearchEmpty vào layout
function loadSearchEmptyLayout() {
    // Gọi đến AJAX để lấy lại layout SearchEmpty
    fetch('/searchEmpty')  // Giả sử bạn có route '/searchEmpty' trả về layout SearchEmpty
        .then(response => response.text())
        .then(html => {
            // Cập nhật lại nội dung của .content-scrollable
            document.querySelector('.content-scrollable').innerHTML = html;

            // Cập nhật URL mà không làm mới trang
            history.pushState(null, '', '/searchEmpty');

            // Cập nhật các script cần thiết sau khi tải lại nội dung
            initializePageScripts();
        })
        .catch(error => console.error('Error loading search empty layout:', error));
}



document.addEventListener('DOMContentLoaded', function () {

    // Sử dụng Event Delegation để xử lý click vào link chi tiết
    document.body.addEventListener('click', function (e) {
        const target = e.target.closest('.details-link');
        if (target) {
            e.preventDefault();
            const url = target.getAttribute('href');

            // Lưu trạng thái player hiện tại
            savePlayerState();

            // Tải nội dung trang chi tiết bằng AJAX
            fetch(url)
                .then(response => response.text())
                .then(html => {
                    // Cập nhật nội dung trang với dữ liệu mới
                    document.querySelector('.content-scrollable').innerHTML = html;
                    // Cập nhật URL mà không làm mới trang
                    history.pushState(null, '', url);

                    // Reinitialize các script và sự kiện
                    initializePageScripts();
                })
                .catch(error => console.error('Error loading details:', error));
        }
    });



    document.body.addEventListener('click', function (e) {
        const target = e.target.closest('.details-link2');
        if (target) {
            e.preventDefault();
            const url = target.getAttribute('href');

            // Lưu trạng thái player hiện tại
            savePlayerState();

            // Tải nội dung trang chi tiết bằng AJAX
            fetch(url)
                .then(response => response.text())
                .then(html => {
                    // Cập nhật nội dung trang với dữ liệu mới
                    document.querySelector('.content-scrollable').innerHTML = html;
                    // Cập nhật URL mà không làm mới trang
                    history.pushState(null, '', url);

                    // Reinitialize các script và sự kiện
                    initializePageScripts();
                })
                .catch(error => console.error('Error loading details:', error));
        }
    });


    document.body.addEventListener('click', function (e) {
        const target = e.target.closest('.details-link3');
        if (target) {
            e.preventDefault();
            const url = target.getAttribute('href');

            // Lưu trạng thái player hiện tại
            savePlayerState();

            // Tải nội dung trang chi tiết bằng AJAX
            fetch(url)
                .then(response => response.text())
                .then(html => {
                    document.querySelector('.content-scrollable').innerHTML = '';

                    // Cập nhật nội dung trang với dữ liệu mới
                    document.querySelector('.content-scrollable').innerHTML = html;
                    // Cập nhật URL mà không làm mới trang
                    history.pushState(null, '', url);

                    // Reinitialize các script và sự kiện
                    initializePageScripts();
                })
                .catch(error => console.error('Error loading details:', error));
        }
    });


    // Khôi phục trạng thái player khi tải trang
    restorePlayerState();

    // Thêm sự kiện lắng nghe cho ô tìm kiếm
    const searchBar = document.querySelector('.search-bar input');
    searchBar.addEventListener('input', function (e) {
        const query = e.target.value.trim();
        const resultsContainer = document.querySelector('.media-grid');
        const searchTitle = document.querySelector('#search-title');

        if (query) {
            fetch(`/Home/Search?query=${encodeURIComponent(query)}`)
                .then(response => response.json())
                .then(data => {
                    console.log('Dữ liệu trả về:', data);

                    // Hiển thị tiêu đề kết quả
                    searchTitle.style.display = 'block';

                    // Xóa kết quả cũ
                    resultsContainer.innerHTML = '';

                    // Truy cập mảng kết quả từ `data.kq.$values`
                    const results = data.kq?.$values || []; // Tránh lỗi nếu không có `kq` hoặc `$values`

                    console.log("Length: " + results.length)
                    // Hiển thị kết quả mới
                    if (results.length > 0) {
                        results.forEach(song => {
                            const songElement = document.createElement('div');
                            songElement.classList.add('media-card');
                            songElement.setAttribute('data-id', song.songID);
                            songElement.setAttribute('data-audio', `/audios/${song.audioFile}`);
                            songElement.setAttribute('data-title', song.title);
                            songElement.setAttribute('data-artist', song.artistName);
                            songElement.setAttribute('data-album', song.albumName);
                            songElement.setAttribute('data-genre', song.genre);
                            songElement.setAttribute('data-duration', song.duration);
                            songElement.setAttribute('data-explicit', song.isExplicit);
                            songElement.innerHTML = `
                        <div class="media-card-image">
                            <a href="/Home/Details/${song.songID}" class="details-link" data-id="${song.songID}">
                                <img src="/pictures/${song.image}" alt="${song.title}" />
                            </a>
                            <button class="play-button" onclick="togglePlay(this)">
                                <i class="bi bi-play-fill"></i>
                            </button>
                        </div>
                        <div class="media-card-content">
                            <h3 class="media-title">${song.title}</h3>
                            <p class="media-description">${song.artistName}</p>
                        </div>                           
                    `;
                            resultsContainer.appendChild(songElement);
                        });
                    } else {
                        // Không có kết quả
                        resultsContainer.innerHTML = '<h5 class="text-danger bold">Không tìm thấy kết quả</h5>';
                        console.error("No songs found or invalid data structure:", data);
                    }
                })
                .catch(error => {
                    // Xử lý lỗi fetch
                    console.error('Error fetching search results:', error);
                    resultsContainer.innerHTML = '<p>An error occurred while searching. Please try again later.</p>';
                });
        } else {
            // Không có query
            searchTitle.style.display = 'none';
            resultsContainer.innerHTML = '';
        }

    });

    // Khởi tạo các script cần thiết
    function initializePageScripts() {
        document.querySelectorAll('.play-button').forEach(button => {
            button.addEventListener('click', function () {
                togglePlay(this);
            });
        });
        console.log('Scripts re-initialized.');
    }
});


function savePlayerState() {
    const state = {
        currentTime: audioPlayer.currentTime,
        isPlaying: !audioPlayer.paused,
        volume: audioPlayer.volume,
        currentTrack: currentlyPlaying ? {
            id: currentlyPlaying.dataset.id,
            title: currentlyPlaying.dataset.title,
            artist: currentlyPlaying.dataset.artist,
            audio: currentlyPlaying.dataset.audio,
            image: currentlyPlaying.querySelector('img').src
        } : null
    };
    localStorage.setItem('playerState', JSON.stringify(state));
}

function restorePlayerState() {
    const state = JSON.parse(localStorage.getItem('playerState'));
    if (state && state.currentTrack) {
        audioPlayer.src = state.currentTrack.audio;
        audioPlayer.currentTime = state.currentTime;
        audioPlayer.volume = state.volume;
        updateNowPlaying(state.currentTrack.title, state.currentTrack.artist, state.currentTrack.image);
        if (state.isPlaying) {
            audioPlayer.play();
            updatePlayPauseButton(true);
        }
        currentlyPlaying = document.querySelector(`.media-card[data-id="${state.currentTrack.id}"]`);
        if (currentlyPlaying) {
            currentlyPlaying.classList.add('playing');
            const playButton = currentlyPlaying.querySelector('.play-button');
            playButton.innerHTML = '<i class="bi bi-pause-fill"></i>';
        }
    }
}

// Thêm event listener cho popstate để xử lý navigation back/forward
window.addEventListener('popstate', function (event) {
    location.reload();
});


/* Details */
