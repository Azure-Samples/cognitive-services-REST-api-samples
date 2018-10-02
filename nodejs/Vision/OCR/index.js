const OCRAPI = require('./API/OCR');
const config = require('./config');

// usage: node index.js <fileLocation> <mode:default=Printed>
const start = async () => {
    const file = process.argv[2];
    const mode = process.argv[3] || 'Printed'; // Handwritten or Printed

    const api = new OCRAPI(config.microsoft.cognitive.vision.key, mode);
    const imageData = await api.readImageByPath(file);
    const result = await api.analyzeImage(imageData);

    console.log(result);
};

start();