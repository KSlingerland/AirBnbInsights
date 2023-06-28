import http from "k6/http";
import { sleep, check } from "k6";

export let options = {
  stages: [
    { duration: "1m", target: 100 }, // Ramp up to 50 virtual users over 1 minute
    { duration: "1m", target: 1500 }, // Stay at 50 virtual users for 1 minutes
    { duration: "30s", target: 0 }   // Ramp down to 0 virtual users over 1 minute
  ],
  thresholds: {
    http_req_duration: [
      { 
        threshold: "p(80)<1000",
        abortOnFail: true,
        delayAbortEval: "5s"
      }
    ]
     // 80% of requests should complete within 1000ms
  }
};

export default function() {
  let response = http.get("HTTPS://localhost:7158/api/listings/5485806");
  
  // Check if the response was successful
  check(response, {
    "is status 200": (r) => r.status === 200
  });
  
  // Simulate think time between requests
  sleep(1);
}