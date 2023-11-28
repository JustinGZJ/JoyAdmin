import { axios } from '@/libs/api.request'
// curl -X 'GET' \
//   'http://127.0.0.1:9001/api/ProcessControl/GetProcessRecordByBarCode/222222' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'
export const GetProcessRecordByBarCode = (code) => {
  return axios.get('api/ProcessControl/GetProcessRecordByBarCode/' + code)
}

// curl -X 'POST' \
//   'http://127.0.0.1:9001/api/ProcessControl/CodeVerify' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "barCode": "222222",
//   "processName": "定子绕线",
//   "productName": "MTB-S"
// }'
export const CodeVerify = (data) => {
  return axios.post('api/ProcessControl/CodeVerify', data)
}
// curl -X 'POST' \
//   'http://127.0.0.1:9001/api/ProcessControl/DataPass' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "barCode": "222222",
//   "result": true,
//   "processName": "定子绕线"
// }'

export const DataPass = (data) => {
  return axios.post('api/ProcessControl/DataPass', data)
}

