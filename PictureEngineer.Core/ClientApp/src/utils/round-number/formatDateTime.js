
export default function formatDate(date){
    var datetimes = new Date(date);
    return datetimes.getDate() + "-" + (datetimes.getMonth() - 1) + "-" + datetimes.getFullYear();
}