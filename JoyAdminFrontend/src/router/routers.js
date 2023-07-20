import Main from '@/components/main'

/**
 * iview-admin中meta除了原生参数外可配置的参数:
 * meta: {
 *  title: { String|Number|Function }
 *         显示在侧边栏、面包屑和标签栏的文字
 *         使用'{{ 多语言字段 }}'形式结合多语言使用，例子看多语言的路由配置;
 *         可以传入一个回调函数，参数是当前路由对象，例子看动态路由和带参路由
 *  hideInBread: (false) 设为true后此级路由将不会出现在面包屑中，示例看QQ群路由配置
 *  hideInMenu: (false) 设为true后在左侧菜单不会显示该页面选项
 *  notCache: (false) 设为true后页面在切换标签后不会缓存，如果需要缓存，无需设置这个字段，而且需要设置页面组件name属性和路由配置的name一致
 *  access: (null) 可访问该页面的权限数组，当前路由设置的权限会影响子路由
 *  icon: (-) 该页面在左侧菜单、面包屑和标签导航处显示的图标，如果是自定义图标，需要在图标名称前加下划线'_'
 *  beforeCloseName: (-) 设置该字段，则在关闭当前tab页时会去'@/router/before-close.js'里寻找该字段名对应的方法，作为关闭前的钩子函数
 * }
 */

export default [
  {
    path: '/login',
    name: 'login',
    meta: {
      title: 'Login - 登录',
      hideInMenu: true
    },
    component: () => import('@/view/login/login.vue')
  },
  {
    path: '/',
    name: '_home',
    redirect: '/home',
    component: Main,
    meta: {
      hideInMenu: true,
      notCache: true
    },
    children: [
      {
        path: '/home',
        name: 'home',
        meta: {
          hideInMenu: true,
          title: '首页',
          notCache: true,
          icon: 'md-home'
        },
        component: () => import('@/view/single-page/home')
      }
    ]
  },
  {
    path: '/dataView',
    name: 'dataView',
    meta: {
      icon: 'ios-paper-outline',
      title: '数据可视化',
      access: ['role', 'auth', 'employee']
    },
    component: Main,
    children: [
      {
        path: '/kanban',
        name: 'kanban',
        meta: {
          icon: 'ios-search',
          title: '总览',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/datav/index.vue'], resolve)
      }, {
        path: '/status',
        name: 'status',
        meta: {
          icon: 'ios-search',
          title: '设备状态',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/datav/MachineStatus.vue'], resolve)
      }, {
        path: '/product',
        name: 'product',
        meta: {
          icon: 'ios-search',
          title: '生产情况',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/datav/ProductView.vue'], resolve)
      }]
  },
  {
    path: '/machine',
    name: 'machine',
    meta: {
      icon: 'ios-paper-outline',
      title: '设备数据',
      access: ['role', 'auth', 'employee']
    },
    component: Main,
    children: [
      {
        path: '/query',
        name: 'query',
        meta: {
          icon: 'ios-search',
          title: '条码查询',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/machine/query.vue'], resolve)
      },
      {
        path: '/queryByStation',
        name: 'queryByStation',
        meta: {
          icon: 'ios-search',
          title: '站位查询',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/machine/TestData.vue'], resolve)
      },
      {
        path: '/queryData',
        name: 'queryData',
        meta: {
          icon: 'ios-search',
          title: '日期查询',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/machine/queryAll.vue'], resolve)
      }
    ]
  },
  {
    path: '/alarm',
    name: 'alarm',
    meta: {
      icon: 'ios-paper-outline',
      title: '报警数据'
    },
    component: Main,
    children: [
      {
        path: '/alarmData',
        name: 'alarmData',
        meta: {
          icon: 'ios-paper',
          title: '报警数据',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/alarms/QueryAlarms.vue'], resolve)
      }, {
        path: '/alarmStatistics',
        name: 'alarmStatistics',
        meta: {
          icon: 'ios-paper',
          title: '报警统计',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/alarms/AlarmStatistics.vue'], resolve)
      }]
  },
  {
    path: '/report',
    name: 'report',
    meta: {
      icon: 'ios-paper-outline',
      title: '报表管理'
    },
    component: Main,
    children: [
      {
        path: '/reportStatistics',
        name: 'reportStatistics',
        meta: {
          icon: 'ios-paper',
          title: '分时良率',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/report/reportPassrate.vue'], resolve)
      }, {
        path: '/stationPassRate',
        name: 'stationPassRate',
        meta: {
          icon: 'ios-paper',
          title: '工位良率',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/report/StationPassRate.vue'], resolve)
      }, {
        path: '/ngDistribution',
        name: 'ngDistribution',
        meta: {
          icon: 'ios-paper',
          title: '不良分布',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/report/NgDistribution.vue'], resolve)
      }]
  },
  {
    name: '/upload',
    path: 'upload',
    meta: {
      icon: 'ios-paper-outline',
      title: '设备上传'
    },
    component: Main,
    children: [ {
      path: '/testdata',
      name: 'testdata',
      meta: {
        icon: 'ios-paper',
        title: '生产数据',
        access: ['role', 'auth', 'employee']
      },
      component: resolve => require(['@/view/machine/data.vue'], resolve)
    },
    {
      path: '/uploadBinding',
      name: 'uploadBinding',
      meta: {
        icon: 'ios-paper',
        title: '绑定信息',
        access: ['role', 'auth', 'employee']
      },
      component: resolve => require(['@/view/machine/codeBinding.vue'], resolve)
    },
    {
      path: '/reportData',
      name: 'reportData',
      meta: {
        icon: 'ios-paper',
        title: '统计数据',
        access: ['role', 'auth', 'employee']
      },
      component: resolve => require(['@/view/report/recentUpload.vue'], resolve)
    }
    ]
  },
  {
    path: '/system',
    name: 'system',
    meta: {
      icon: 'md-settings',
      title: '系统管理',
      access: ['role', 'auth', 'employee']
    },
    component: Main,
    children: [
      {
        path: '/auth',
        name: 'auth',
        meta: {
          icon: 'ios-paper',
          title: '权限管理',
          access: ['auth']
        },
        component: resolve => require(['@/view/system/auth.vue'], resolve)
      },
      {
        path: '/role',
        name: 'role',
        meta: {
          icon: 'md-document',
          title: '角色管理',
          access: ['role']
        },
        component: resolve => require(['@/view/system/role.vue'], resolve)
      },
      {
        path: '/employee',
        name: 'employee',
        meta: {
          icon: 'ios-people',
          title: '员工管理',
          access: ['employee']
        },
        component: resolve => require(['@/view/system/employee.vue'], resolve)
      }
    ]
  },
  {
    name: 'maintenance',
    path: '/maintenance',
    meta: {
      icon: 'ios-paper-outline',
      title: '人工操作'
    },
    component: Main,
    children: [
      {
        path: '/fault-handing',
        name: 'fault-handing',
        meta: {
          icon: 'ios-paper',
          title: '故障操作',
          access: ['role', 'auth', 'employee']
        },
        component: resolve => require(['@/view/Maintenance/FaultHandling.vue'], resolve)
      }
    ]
  },
  {
    path: '/updatepwd',
    name: 'updatepwd',
    meta: {
      hideInMenu: true,
      hideInBread: true
    },
    component: Main,
    children: [
      {
        path: '/updatepwd',
        name: 'updatepwd',
        meta: {
          icon: 'ios-unlock',
          title: '修改密码'
        },
        component: resolve => require(['@/view/updatepwd/updatepwd.vue'], resolve)
      }
    ]
  },
  {
    path: '/refresh',
    name: 'refresh',
    meta: {
      hideInMenu: true,
      hideInBread: true
    },
    // component: Main,
    component: resolve => require(['@/view/refresh/refresh.vue'], resolve)
  },
  {
    path: '/401',
    name: 'error_401',
    meta: {
      hideInMenu: true
    },
    component: () => import('@/view/error-page/401.vue')
  },
  {
    path: '/500',
    name: 'error_500',
    meta: {
      hideInMenu: true
    },
    component: () => import('@/view/error-page/500.vue')
  },
  {
    path: '*',
    name: 'error_404',
    meta: {
      hideInMenu: true
    },
    component: () => import('@/view/error-page/404.vue')
  }
]
