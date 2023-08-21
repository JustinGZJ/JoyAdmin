import { axios } from '@/libs/api.request'

export const FilterProductList = (params) => {
  return axios.post('api/Product/FilterList', params)
}
// 获取Product列表
export function getProductList () {
  return axios.get('/api/Product/GetList')
}

// 获取单个Product
export function getProduct (id) {
  return axios.get(`/api/Product/GetOne/${id}`)
}

// 更新Product
export function updateProduct (data) {
  return axios.post('/api/Product/Update', data)
}

// 删除Product
export function deleteProduct (id) {
  return axios.delete(`/api/Product/Delete/${id}`)
}

// 新增Product
export function addProduct (data) {
  return axios.post('/api/Product/Add', data)
}

export default {
  FilterProductList,
  getProductList,
  getProduct,
  updateProduct,
  deleteProduct,
  addProduct
}
