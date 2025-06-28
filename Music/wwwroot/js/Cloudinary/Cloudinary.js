"use strict";

async function uploadChunk(chunk) {
    const formData = new FormData();
    chunk.forEach(file => formData.append('files', file));

    const response = await fetch('/Cloudinary/LoadArrayFiles', {
        method: 'POST',
        body: formData
    });

    if (!response.ok) {
        throw new Error(`Ошибка загрузки: ${response.statusText}`);
    }
    return await response.json();
}
function splitFilesIntoChunks(files, chunkSize) {
    const chunks = [];
    for (let i = 0; i < files.length; i += chunkSize) {
        chunks.push(Array.from(files).slice(i, i + chunkSize));
    }
    return chunks;
}

async function uploadAllFiles(files) {
    const chunks = splitFilesIntoChunks(files, 2); // Пачки по 2 файла
    const results = [];
    let uploadedCount = 0;

    for (const chunk of chunks) {
        try {
            const chunkResult = await uploadChunk(chunk);
            results.push(...chunkResult);
            uploadedCount += chunk.length;

            updateProgress((uploadedCount / files.length) * 100);

        } catch (error) {
            console.error('Ошибка в пачке:', error);
            break;
        }
    }

    return results;
}

function updateProgress(percent) {
    const progressContainer = document.querySelector('.progress');
    const progressBar = document.getElementById('uploadProgress');

    if (progressContainer && progressBar) {
        progressContainer.classList.remove('d-none'); 
        progressBar.style.width = `${percent}%`;
    }
}
