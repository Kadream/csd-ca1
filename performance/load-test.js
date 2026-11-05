import http from 'k6/http';
import { check, sleep } from 'k6';

// Load test: 20 virtual users visit the home page for 30 seconds
export const options = {
  vus: 20,
  duration: '30s',
  thresholds: {
    http_req_failed: ['rate<0.01'],   // less than 1% of requests may fail
    http_req_duration: ['p(95)<500'], // 95% of requests must finish within 500 ms
  },
};

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';

export default function () {
  const res = http.get(`${BASE_URL}/`);
  check(res, { 'status is 200': (r) => r.status === 200 });
  sleep(1);
}