import { axios } from '@/libs/api.request'

// curl -X 'POST' \
//   'http://localhost:9001/api/MeritPay/FilterList' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjUzODE5MywibmJmIjoxNjkyNTM4MTkzLCJleHAiOjE2OTI3OTczOTMsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.kOqjRXq2dNYGw8aiP6Gfa0MSoXGY6y3JRBtoeaTFiaY' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "page": 0,
//   "size": 0,
//   "filterProperty": "string",
//   "filterValue": "string",
//   "sortProperty": "string",
//   "desc": true
// }'

export const FilterProcessList = (params) => {
  return axios.post('api/Process/FilterList', params)
}

// 获取Process列表
export function getProcessList () {
  return axios.get('/api/Process/GetList')
}

// 获取单个Process
export function getProcess (id) {
  return axios.get(`/api/Process/GetOne/${id}`)
}

// 更新Process
export function updateProcess (data) {
  return axios.post('/api/Process/Update', data)
}

// 删除Process
export function deleteProcess (id) {
  return axios.delete(`/api/Process/Delete/${id}`)
}

// 新增Process
export function addProcess (data) {
  return axios.post('/api/Process/Add', data)
}
