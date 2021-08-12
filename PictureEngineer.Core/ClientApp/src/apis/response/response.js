export default function responseStatus(code, message, data, errors) {
    return {
        code: code,
        message: message,
        errors: errors,
        data: data
    }
}