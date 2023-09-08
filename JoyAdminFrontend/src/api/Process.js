import { axios } from '@/libs/api.request'

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

export const getStations = async () => {
  const response = await getProcessList()
  // get list 并提取其中的 ProcessName
  const { Succeeded, Data, Errors, StatusCode } = response.data
  if (Succeeded) {
    const stations = Data.map(item => item.ProcessName)
    return { status: StatusCode, data: stations }
  } else {
    return { status: StatusCode, message: Errors }
  }
}

export default {
  FilterProcessList,
  getProcessList,
  getProcess,
  updateProcess,
  deleteProcess,
  addProcess
}
