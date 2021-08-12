export default async function fileToBase64(image) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()
        reader.onload = value => {
            resolve({ success: true, data: reader.result })
        }
        reader.onError = error => {
            resolve({ success: false, error })
        }
        reader.readAsDataURL(image)
    })
}