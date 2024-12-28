let searchTimeout;
const searchInput = document.getElementById('searchInput');
const searchResults = document.getElementById('searchResults');

searchInput.addEventListener('input', function () {
    clearTimeout(searchTimeout);
    searchTimeout = setTimeout(() => {
        const query = this.value.trim();
        if (query.length > 1) {
            performSearch(query);
        } else {
            searchResults.innerHTML = '';
            searchResults.style.display = 'none';
        }
    }, 300);
});

async function performSearch(query) {
    try {
        const response = await fetch(`/Home/Search?query=${encodeURIComponent(query)}`);
        const data = await response.json();

        if (data.success) {
            displaySearchResults(data.results);
        } else {
            searchResults.innerHTML = '<p>Không tìm thấy kết quả.</p>';
            searchResults.style.display = 'block';
        }
    } catch (error) {
        console.error('Error during search:', error);
        searchResults.innerHTML = '<p>Đã xảy ra lỗi khi tìm kiếm.</p>';
        searchResults.style.display = 'block';
    }
}

function displaySearchResults(results) {
    if (results.length === 0) {
        searchResults.innerHTML = '<p>Không tìm thấy kết quả.</p>';
    } else {
        const resultsHtml = results.map(song => `
            <div class="search-result-item" data-id="${song.id}" data-audio="/audios/${song.audioFile}" data-title="${song.title}" data-artist="${song.artistName}">
                <img src="/pictures/${song.image}" alt="${song.title}" class="search-result-image">
                <div class="search-result-info">
                    <h3>${song.title}</h3>
                    <p>${song.artistName}</p>
                </div>
                <button class="play-button">
                    <i class="bi bi-play-fill"></i>
                </button>
            </div>
        `).join('');

        searchResults.innerHTML = resultsHtml;
    }
    searchResults.style.display = 'block';
}

searchResults.addEventListener('click', function (e) {
    const playButton = e.target.closest('.play-button');
    if (playButton) {
        const resultItem = playButton.closest('.search-result-item');
        const audioSrc = resultItem.dataset.audio;
        const title = resultItem.dataset.title;
        const artist = resultItem.dataset.artist;
        const image = resultItem.querySelector('img').src;

        // Gọi hàm togglePlay từ audio-player.js
        togglePlay({
            closest: () => ({
                dataset: {
                    audio: audioSrc,
                    title: title,
                    artist: artist,
                    id: resultItem.dataset.id
                },
                querySelector: () => ({ src: image })
            })
        });

        // Ẩn kết quả tìm kiếm sau khi chọn bài hát
        searchResults.style.display = 'none';
        searchInput.value = '';
    }
});

// Ẩn kết quả tìm kiếm khi click bên ngoài
document.addEventListener('click', function (e) {
    if (!searchResults.contains(e.target) && e.target !== searchInput) {
        searchResults.style.display = 'none';
    }
});