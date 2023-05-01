import axios from 'axios'
import { baseUrl } from '../constants/constants'

export const getAllBlogs = () => axios.get(`${baseUrl}/blogs`)

export const getTopBlogs = () => axios.get(`${baseUrl}/blogs/top`)

export const getBlog = (id) => axios.get(`${baseUrl}/blogs/article/${id}`)

export const addLike = (id) => axios.put(`${baseUrl}/blogs/like/${id}`)

export const getComments = (id) => axios.get(`${baseUrl}/blogs/comments/${id}`)

export const addComment = (id, payload) => axios.post(`${baseUrl}/blogs/comment/${id}`, payload)
