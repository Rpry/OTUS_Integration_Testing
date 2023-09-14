import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 1000,
    duration: '2s',
};
export default function () {
    const res = http.get('http://localhost:5200/Course/1');
    check(res, { 'status was 200': (r) => r.status === 200 });
}
