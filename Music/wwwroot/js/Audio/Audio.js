﻿"use strict";

class FactoryElement {

    static createTrackElement(song) {
        return `       
             <div class="track-item d-flex align-items-center justify-content-between" data-src="${song.url}">
                <div class="d-flex align-items-center flex-grow-1">
                    <div class="track-number">•</div>
                    <div class="track-info ms-3">
                        <h6 class="track-info-title mb-0">${song.fileName}</h6>
                    </div>
                </div>
    
                <div class="track-actions d-flex align-items-center">
                    <!-- Кнопка скачивания -->
                    <a href="/Cloudinary/Song?url=${song.url}" class="btn-download me-2" title="Скачать">
                        <i class="bi bi-download"></i>
                    </a>
        
                    <!-- Кнопка добавления в избранное -->
                    <button class="btn-favorite" title="Добавить в избранное" data-song-id="${song.id}">
                        <i class="bi bi-heart"></i>
                    </button>
                </div>
            </div>
        `;
    }

    static createSongField() {
        return `
         <div class="song-input mb-3 col-6">
                      <div class="w-100">
                        <!-- Стилизованная кнопка-->
                        <label for="inputFile" class="btn btn-outline-primary w-100 mt-2 songLabel">
                          <i class="bi bi-upload me-2"></i>
                          Выбрать песни
                        </label>

                        <!-- Сам инпут (скрыт) -->
                        <input type="file"
                               name="files"
                               id="inputFile"
                               class="d-none"
                               accept="audio/*"
                               multiple>
                      </div>

                       <!-- Список выбранных файлов -->
                        <ul class="file-names mt-2 list-unstyled text-light small"></ul>


                      <button type="button" class="btn btn-success saveSong mt-3 d-none">
                          <i class="bi bi-cloud-upload"></i>
                    </button>
                    </div>
        `;
    }
}

//Управление плеером
$(document).ready(function () {
    const player = $('#globalPlayer')[0];
    let currentTrack = null;
    let seekInterval;
    $('#playBtn').hide();
    // Форматирование времени (секунды -> MM:SS)
    function formatTime(seconds) {
        const mins = Math.floor(seconds / 60);
        const secs = Math.floor(seconds % 60);
        return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
    }

    // Обновление прогресса
    function updateProgress() {
        $('#currentTime').text(formatTime(player.currentTime));
        $('#progressBar').val(player.currentTime / player.duration * 100 || 0);
    }

    // Открытие плеера при выборе трека
    $('.track-item').click(function () {
        $(".player-controls-container").removeClass('d-none');
        const trackSrc = $(this).data('src');
        const trackTitle = $(this).find('.track-info-title').text();

        if (currentTrack !== trackSrc) {
            player.src = trackSrc;
            player.play()
                .then(() => {
                    currentTrack = trackSrc;
                    $('.track-item').removeClass('playing');
                    $(this).addClass('playing');
                    $('#nowPlaying').text(trackTitle);

                    // Обновляем длительность при загрузке метаданных
                    player.addEventListener('loadedmetadata', function () {
                        $('#duration').text(formatTime(player.duration));
                    });

                    // Запускаем обновление прогресса
                    clearInterval(seekInterval);
                    seekInterval = setInterval(updateProgress, 1000);
                });
        } else {
            player.paused ? player.play() : player.pause();
        }
    });

    // Перемотка при изменении прогресс-бара
    $('#progressBar').on('input', function () {
        const percent = $(this).val();
        player.currentTime = (percent / 100) * player.duration;
    });

    // Управление плеером
    $('#playBtn').click(() => {
        player.play();
        $('#playBtn').hide();
        $('#pauseBtn').show();
    });

    $('#pauseBtn').click(() => {
        player.pause();
        $('#playBtn').show();
        $('#pauseBtn').hide();
    });

    // Закрытие плеера
    $('#closeBtn').click(function (e) {
        e.stopPropagation();
        $(".player-controls-container").addClass('d-none');
        player.pause();
        $('.track-item').removeClass('playing');
        clearInterval(seekInterval);
    });

    // При завершении трека
    player.addEventListener('ended', function () {
        $('.track-item').removeClass('playing');
        $('#nowPlaying').text('Трек завершен');
        $('#playBtn').show();
        $('#pauseBtn').hide();
        clearInterval(seekInterval);
    });
});


