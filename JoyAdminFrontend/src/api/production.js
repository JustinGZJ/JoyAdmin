import { axios } from '@/libs/api.request'

// curl -X 'POST' \
//   'http://localhost:9001/api/Statistic/Upload' \
//   -H 'accept: */*' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Device": "定子1",
//   "ProductionType": 0,
//   "Quantity": 1,
//   "Reason": "无撒地方",
//   "Count": 1
// }'

export const Upload = (data) => {
  return axios.post('api/Statistic/Upload', data)
}

// curl -X 'GET' \
//   'http://localhost:9001/api/Statistic/GetRecentUpload/1/50' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'

export const GetRecentUpload = (page, size) => {
  return axios.get(`api/Statistic/GetRecentUpload/${page}/${size}`)
}

// curl -X 'POST' \
//   'http://localhost:9001/api/Statistic/GetPassRates' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Start": "2023-06-12T03:07:07.637Z",
//   "End": "2023-06-12T03:07:07.637Z",
//   "Device": "string",
//   "Aggregation": 0
// }'
export const GetPassRates = (data) => {
  return axios.post('api/Statistic/GetPassRates', data)
}

// curl -X 'GET' \
//   'http://localhost:9001/api/Statistic/GetPassRateByDevice/2021-01-01/2024-01-01' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'

export const GetPassRateByDevice = (from, to) => {
  return axios.get(`api/Statistic/GetPassRateByDevice/${from}/${to}`)
}

// curl -X 'POST' \
//   'http://localhost:9001/api/Statistic/QueryNgCounts' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Start": "2023-06-12T05:19:36.269Z",
//   "End": "2023-06-12T05:19:36.269Z",
//   "Device": "string",
//   "Aggregation": 0,
//   "Limit": 0
// }'
const QueryNgCounts = (data) => {
  return axios.post(`api/Statistic/QueryNgCounts`, data)
}
export const GetPassRateByWorkOrder = (workOrder) => {
  return axios.get(`api/Statistic/GetPassRateByWorkOrder/${workOrder}`)
}

export default { Upload, GetRecentUpload, GetPassRates, GetPassRateByDevice, QueryNgCounts, GetPassRateByWorkOrder }
