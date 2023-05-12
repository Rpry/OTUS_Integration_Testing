import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 100,
    duration: '10s',
};
export default function () {
    const res = http.get('http://localhost:5000/Course/1');
    check(res, { 'status was 200': (r) => r.status === 200 });
}
