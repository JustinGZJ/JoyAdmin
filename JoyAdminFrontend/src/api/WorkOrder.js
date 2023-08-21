import { axios } from '@/libs/api.request'
// curl -X 'GET' \
//   'http://localhost:9001/api/WorkOrder/GetWorkOrders/1/50' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjMyNzcyOCwibmJmIjoxNjkyMzI3NzI4LCJleHAiOjE2OTI1ODY5MjgsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.pRIvwhVG7hP4eO1zW09IhpiH3QhwnvqcCz7F30AiUKc' \
//   -H 'request-from: swagger'
export const GetWorkOrders = (page, pageSize) => {
  return axios.get(`api/WorkOrder/GetWorkOrders/${page}/${pageSize}`)
}

// curl -X 'GET' \
//   'http://localhost:9001/api/WorkOrder/GetWorkOrderById/1' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjMyNzcyOCwibmJmIjoxNjkyMzI3NzI4LCJleHAiOjE2OTI1ODY5MjgsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.pRIvwhVG7hP4eO1zW09IhpiH3QhwnvqcCz7F30AiUKc' \
//   -H 'request-from: swagger'

export const GetWorkOrderById = (id) => {
  return axios.get(`api/WorkOrder/GetWorkOrderById/${id}`)
}

// curl -X 'POST' \
//   'http://localhost:9001/api/WorkOrder/AddWorkOrder' \
//   -H 'accept: */*' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjMyNzcyOCwibmJmIjoxNjkyMzI3NzI4LCJleHAiOjE2OTI1ODY5MjgsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.pRIvwhVG7hP4eO1zW09IhpiH3QhwnvqcCz7F30AiUKc' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "CreatedTime": "2023-08-18T05:39:38.824Z",
//   "UpdatedTime": "2023-08-18T05:39:38.824Z",
//   "WorkOrderNo": "string",
//   "ProductNo": "string",
//   "ProductName": "string",
//   "PlanQuantity": 0,
//   "ActualQuantity": 0,
//   "StartTime": "2023-08-18T05:39:38.824Z",
//   "FinishTime": "2023-08-18T05:39:38.824Z",
//   "Status": "string"
// }'

export const AddWorkOrder = (data) => {
  return axios.post(`api/WorkOrder/AddWorkOrder`, data)
}

// curl -X 'POST' \
//   'http://localhost:9001/api/WorkOrder/UpdateWorkOrder' \
//   -H 'accept: */*' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjMyNzcyOCwibmJmIjoxNjkyMzI3NzI4LCJleHAiOjE2OTI1ODY5MjgsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.pRIvwhVG7hP4eO1zW09IhpiH3QhwnvqcCz7F30AiUKc' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Id": 1,
//   "CreatedTime": "2023-08-18T05:41:05.399Z",
//   "UpdatedTime": "2023-08-18T05:41:05.399Z",
//   "WorkOrderNo": "string",
//   "ProductNo": "string",
//   "ProductName": "string",
//   "PlanQuantity": 0,
//   "ActualQuantity": 0,
//   "StartTime": "2023-08-18T05:41:05.399Z",
//   "FinishTime": "2023-08-18T05:41:05.399Z",
//   "Status": "string"
// }'

export const UpdateWorkOrder = (data) => {
  return axios.post(`api/WorkOrder/UpdateWorkOrder`, data)
}
// curl -X 'DELETE' \
//   'http://localhost:9001/api/WorkOrder/DeleteWorkOrder/1' \
//   -H 'accept: */*' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY5MjMyNzcyOCwibmJmIjoxNjkyMzI3NzI4LCJleHAiOjE2OTI1ODY5MjgsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.pRIvwhVG7hP4eO1zW09IhpiH3QhwnvqcCz7F30AiUKc' \
//   -H 'request-from: swagger'

export const DeleteWorkOrder = (id) => {
  return axios.delete(`api/WorkOrder/DeleteWorkOrder/${id}`)
}

export default {
  GetWorkOrders,
  GetWorkOrderById,
  AddWorkOrder,
  UpdateWorkOrder,
  DeleteWorkOrder
}
