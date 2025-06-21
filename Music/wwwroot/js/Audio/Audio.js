"use strict";

class FactoryElement {

    static createTrackElement(index, song) {
        return `
            <div class="track-item" data-track-id="${index}">
                <div class="track-number text-light">${index}</div>
                <div class="track-info col-4">
                    <h6 class="mb-0 text-light">${song.fileName}</h6>
                </div>
                <audio controls class="audio-player col-8">
                    <source src="${song.url}" type="audio/mpeg">
                    Ваш браузер не поддерживает аудио элемент.
                </audio>
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


